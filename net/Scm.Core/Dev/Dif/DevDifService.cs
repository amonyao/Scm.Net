using Com.Scm.Config;
using Com.Scm.Dev.Dif.Dvo;
using Com.Scm.Enums;
using Com.Scm.Exceptions;
using Com.Scm.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Text;

namespace Com.Scm.Dev.Dif
{
    /// <summary>
    /// 数据库差异
    /// </summary>
    [ApiExplorerSettings(GroupName = "Dev")]
    public class DevDifService : ApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="envConfig"></param>
        public DevDifService(EnvConfig envConfig)
        {
            _EnvConfig = envConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public void Test()
        {
            var request = new UpgradeRequest();
            request.SrcHost = "localhost";
            request.SrcPort = 3306;
            request.SrcUser = "root";
            request.SrcPass = "123456";
            request.SrcDb = "scm";

            request.DstHost = "localhost";
            request.DstPort = 3306;
            request.DstUser = "root";
            request.DstPass = "123456";
            request.DstDb = "scm_pub";

            Upgrade(request);
        }

        /// <summary>
        /// 数据库比较
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CompareResult> Compare(CompareRequest request)
        {
            try
            {
                var srcConfig = GetConfig(request.SrcHost, request.SrcPort, request.SrcUser, request.SrcPass, request.SrcDb);
                var srcClient = new SqlSugarClient(srcConfig);
                srcClient.Open();

                var dstConfig = GetConfig(request.DstHost, request.DstPort, request.DstUser, request.DstPass, request.DstDb);
                var dstClient = new SqlSugarClient(dstConfig);
                dstClient.Open();

                return CompareTable(srcClient, dstClient, request.comment);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 数据库更新
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public List<UpgradeResult> Upgrade(UpgradeRequest request)
        {
            try
            {
                var srcConfig = GetConfig(request.SrcHost, request.SrcPort, request.SrcUser, request.SrcPass, request.SrcDb);
                var srcClient = new SqlSugarClient(srcConfig);
                srcClient.Open();

                var dstConfig = GetConfig(request.DstHost, request.DstPort, request.DstUser, request.DstPass, request.DstDb);
                var dstClient = new SqlSugarClient(dstConfig);
                dstClient.Open();

                var list = UpgradeTable(srcClient, dstClient, request.comment);
                var file = _EnvConfig.GetTempPath(DateTime.Now.ToFileTimeUtc() + ".sql");
                using (var writer = new StreamWriter(file))
                {
                    foreach (var item in list)
                    {
                        writer.WriteLine($"/**{item.Table}*/");
                        foreach (var sql in item.Sqls)
                        {
                            writer.WriteLine(sql);
                        }
                        writer.WriteLine();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        private ConnectionConfig GetConfig(string host, int port, string user, string pass, string db)
        {
            var config = new ConnectionConfig();
            config.ConnectionString = $"server={host};database={db};uid={user};pwd={pass};charset=utf8mb4;SslMode=None;";
            config.DbType = DbType.MySql;
            config.IsAutoCloseConnection = true;
            config.InitKeyType = InitKeyType.Attribute;
            return config;
        }

        #region 表
        /// <summary>
        /// 表比较
        /// </summary>
        private List<CompareResult> CompareTable(ISqlSugarClient srcClient, ISqlSugarClient dstClient, bool hasComment)
        {
            var srcTables = srcClient.DbMaintenance.GetTableInfoList();
            var dstTables = dstClient.DbMaintenance.GetTableInfoList();

            var results = new List<CompareResult>();
            DevDifEnum dif;
            foreach (var srcTable in srcTables)
            {
                var dstTable = GetTable(dstTables, srcTable);
                if (dstTable == null)
                {
                    dif = DevDifEnum.Create;
                    results.Add(new CompareResult { Table = srcTable.Name, Dif = dif });
                    continue;
                }

                var result = CompareColumn(srcClient, srcTable, dstClient, dstTable, hasComment);
                if (result != DevDifEnum.None)
                {
                    dif = DevDifEnum.Update;
                    results.Add(new CompareResult { Table = dstTable.Name, Dif = dif });
                }
                dstTables.Remove(dstTable);
            }

            foreach (var dstTable in dstTables)
            {
                dif = DevDifEnum.Delete;
                results.Add(new CompareResult { Table = dstTable.Name, Dif = dif });
            }

            return results;
        }

        /// <summary>
        /// 表升级
        /// </summary>
        private List<UpgradeResult> UpgradeTable(ISqlSugarClient srcClient, ISqlSugarClient dstClient, bool hasComment)
        {
            var srcTables = srcClient.DbMaintenance.GetTableInfoList();
            var tmp = dstClient.DbMaintenance.GetTableInfoList();
            var dstTables = new List<DbTableInfo>();
            dstTables.AddRange(tmp);

            var results = new List<UpgradeResult>();
            List<string> sqls;
            foreach (var srcTable in srcTables)
            {
                var dstTable = GetTable(dstTables, srcTable);
                if (dstTable == null)
                {
                    var sql = GenCreateTable(srcClient, srcTable, hasComment);
                    sqls = new List<string> { sql };
                    results.Add(new UpgradeResult { Table = srcTable.Name, Sqls = sqls });
                    continue;
                }

                sqls = UpgradeColumn(srcClient, srcTable, dstClient, dstTable, hasComment);
                if (sqls.Count > 0)
                {
                    results.Add(new UpgradeResult { Table = srcTable.Name, Sqls = sqls });
                }

                UpgradeKey(srcClient, srcTable, dstClient, dstTable);
                UpgradeIndex(srcClient, srcTable, dstClient, dstTable);

                dstTables.Remove(dstTable);
            }

            foreach (var dstTable in dstTables)
            {
                var sql = GenDropTable(dstTable);
                sqls = new List<string> { sql };
                results.Add(new UpgradeResult { Table = dstTable.Name, Sqls = sqls });
            }

            return results;
        }

        /// <summary>
        /// 表比较
        /// </summary>
        private string CompareTable(ISqlSugarClient srcClient, ISqlSugarClient dstClient, string table)
        {
            //var srcTables = srcClient.DbMaintenance.GetColumnInfosByTableName(table);
            //var dstTables = dstClient.DbMaintenance.GetColumnInfosByTableName(table);

            //var sql = new StringBuilder();
            //if (dstTable == null)
            //{
            //    sql.Append(GenCreateTable(srcClient, srcTable));
            //    continue;
            //}

            //CompareColumn(srcClient, srcTable, dstClient, dstTable);
            //dstTables.Remove(dstTable);

            //foreach (var dstTable in dstTables)
            //{
            //    sql.Append(GenDropTable(dstTable));
            //}

            return "";
        }

        /// <summary>
        /// 获取表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private DbTableInfo GetTable(List<DbTableInfo> list, DbTableInfo table)
        {
            var srcName = table.Name.ToLower();
            foreach (var tableInfo in list)
            {
                if (tableInfo.Name.ToLower() == srcName)
                {
                    return tableInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 删除表格
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GenDropTable(DbTableInfo table)
        {
            var sql = new StringBuilder();
            sql.Append("DROP TABLE ").Append(table.Name);
            sql.Append(";");

            return sql.ToString();
        }

        /// <summary>
        /// 添加表格
        /// </summary>
        /// <param name="client"></param>
        /// <param name="table"></param>
        /// <param name="hasComment"></param>
        /// <returns></returns>
        private string GenCreateTable(ISqlSugarClient client, DbTableInfo table, bool hasComment)
        {
            var columns = client.DbMaintenance.GetColumnInfosByTableName(table.Name);

            var sql = new StringBuilder();
            sql.Append("CREATE TABLE ").Append(table.Name);
            sql.Append("(");
            foreach (var column in columns)
            {
                sql.Append(GenColumn(column, hasComment)).Append(",");
            }
            if (sql[sql.Length - 1] == ',')
            {
                sql.Remove(sql.Length - 1, 1);
            }
            sql.Append(");");

            return sql.ToString();
        }
        #endregion

        #region 列
        /// <summary>
        /// 列比较
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="srcTable"></param>
        /// <param name="dstClient"></param>
        /// <param name="dstTable"></param>
        /// <param name="hasComment"></param>
        private DevDifEnum CompareColumn(ISqlSugarClient srcClient, DbTableInfo srcTable, ISqlSugarClient dstClient, DbTableInfo dstTable, bool hasComment)
        {
            var srcColumns = srcClient.DbMaintenance.GetColumnInfosByTableName(srcTable.Name);
            var dstColumns = dstClient.DbMaintenance.GetColumnInfosByTableName(dstTable.Name);

            var result = DevDifEnum.None;
            foreach (var srcColumn in srcColumns)
            {
                var dstColumn = GetColumn(dstColumns, srcColumn);
                if (dstColumn == null)
                {
                    result = DevDifEnum.Create;
                    continue;
                }

                if (IsColumnChanged(srcColumn, dstColumn, hasComment))
                {
                    result = DevDifEnum.Update;
                }
                dstColumns.Remove(dstColumn);
            }

            foreach (var dstColumn in dstColumns)
            {
                result = DevDifEnum.Delete;
            }
            return result;
        }

        /// <summary>
        /// 列升级
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="srcTable"></param>
        /// <param name="dstClient"></param>
        /// <param name="dstTable"></param>
        /// <param name="hasComment"></param>
        private List<string> UpgradeColumn(ISqlSugarClient srcClient, DbTableInfo srcTable, ISqlSugarClient dstClient, DbTableInfo dstTable, bool hasComment)
        {
            var srcColumns = srcClient.DbMaintenance.GetColumnInfosByTableName(srcTable.Name);
            var dstColumns = dstClient.DbMaintenance.GetColumnInfosByTableName(dstTable.Name);

            var sqls = new List<string>();
            foreach (var srcColumn in srcColumns)
            {
                var dstColumn = GetColumn(dstColumns, srcColumn);
                if (dstColumn == null)
                {
                    sqls.Add(GenCreateColumn(dstTable, srcColumn, hasComment));
                    continue;
                }

                if (IsColumnChanged(srcColumn, dstColumn, hasComment))
                {
                    sqls.Add(GenModifyColumn(dstTable, srcColumn, hasComment));
                }
                dstColumns.Remove(dstColumn);
            }

            foreach (var dstColumn in dstColumns)
            {
                sqls.Add(GenDropColumn(dstTable, dstColumn));
            }

            return sqls;
        }

        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="list"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private DbColumnInfo GetColumn(List<DbColumnInfo> list, DbColumnInfo column)
        {
            var srcName = column.DbColumnName.ToLower();
            foreach (var tableInfo in list)
            {
                if (tableInfo.DbColumnName.ToLower() == srcName)
                {
                    return tableInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GenDropColumn(DbTableInfo table, DbColumnInfo column)
        {
            //ALTER TABLE scm.scm_qcs_detail DROP COLUMN idx;
            var sql = new StringBuilder();
            sql.Append("ALTER TABLE ").Append(table.Name);
            sql.Append(" DROP COLUMN");
            sql.Append(" ").Append(column.DbColumnName);
            sql.Append(";");

            return sql.ToString();
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="hasComment"></param>
        /// <returns></returns>
        private string GenModifyColumn(DbTableInfo table, DbColumnInfo column, bool hasComment)
        {
            //ALTER TABLE scm.scm_qcs_detail CHANGE idx idx0 int(11) DEFAULT NULL NULL COMMENT '当前号码';
            //ALTER TABLE scm.scm_qcs_detail MODIFY COLUMN idx int(11) DEFAULT NULL NULL COMMENT '当前号码0';
            var sql = new StringBuilder();
            sql.Append("ALTER TABLE ").Append(table.Name);
            sql.Append(" MODIFY COLUMN");
            sql.Append(GenColumn(column, hasComment));
            sql.Append(";");

            return sql.ToString();
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <returns></returns>
        private string GenCreateColumn(DbTableInfo table, DbColumnInfo column, bool hasComment)
        {
            // ALTER TABLE scm.scm_qcs_detail ADD Column1 varchar(100) NULL;

            var sql = new StringBuilder();
            sql.Append("ALTER TABLE ").Append(table.Name);
            sql.Append(" ADD");
            sql.Append(GenColumn(column, hasComment));
            sql.Append(";");

            return sql.ToString();
        }
        #endregion

        #region 主键
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="srcTable"></param>
        /// <param name="dstClient"></param>
        /// <param name="dstTable"></param>
        /// <returns></returns>
        private List<string> UpgradeKey(ISqlSugarClient srcClient, DbTableInfo srcTable, ISqlSugarClient dstClient, DbTableInfo dstTable)
        {
            //var srcKeys = srcClient.DbMaintenance.GetPrimaries(srcTable.Name);
            //var dstKeys = dstClient.DbMaintenance.GetPrimaries(dstTable.Name);

            //foreach (var srcKey in srcKeys)
            //{
            //    if (srcKey != null)
            //    {

            //    }
            //}
            return null;
        }
        #endregion

        #region 索引
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="srcTable"></param>
        /// <param name="dstClient"></param>
        /// <param name="dstTable"></param>
        /// <returns></returns>
        private List<string> UpgradeIndex(ISqlSugarClient srcClient, DbTableInfo srcTable, ISqlSugarClient dstClient, DbTableInfo dstTable)
        {
            //var srcIndexes = srcClient.DbMaintenance.GetIndexList(srcTable.Name);
            //var dstIndexes = dstClient.DbMaintenance.GetIndexList(dstTable.Name);

            //foreach (var srcIndex in srcIndexes)
            //{
            //}
            return null;
        }

        private string GenCreateIndex(DbTableInfo table)
        {
            //CREATE INDEX scm_qcs_queue_detail_id_IDX USING BTREE ON scm.scm_qcs_queue (detail_id);
            return "";
        }
        #endregion

        /// <summary>
        /// 是否有更新
        /// </summary>
        /// <param name="srcColumn"></param>
        /// <param name="dstColumn"></param>
        /// <param name="hasComment"></param>
        /// <returns></returns>
        private bool IsColumnChanged(DbColumnInfo srcColumn, DbColumnInfo dstColumn, bool hasComment)
        {
            if (dstColumn == null)
            {
                return true;
            }
            if (srcColumn.DbColumnName != dstColumn.DbColumnName
                || srcColumn.DataType != dstColumn.DataType
                || srcColumn.Length != dstColumn.Length
                || srcColumn.IsNullable != dstColumn.IsNullable
                || srcColumn.DefaultValue != dstColumn.DefaultValue)
            {
                return true;
            }

            if (hasComment && srcColumn.ColumnDescription != dstColumn.ColumnDescription)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 生成列信息
        /// </summary>
        /// <param name="column"></param>
        /// <param name="hasComment"></param>
        /// <returns></returns>
        private string GenColumn(DbColumnInfo column, bool hasComment)
        {
            var sql = new StringBuilder();
            sql.Append(" ").Append(column.DbColumnName);

            // DataType
            var tmp = column.DataType;
            sql.Append($" {tmp}({column.Length})");

            // NULL
            sql.Append(" ").Append(column.IsNullable ? "NULL" : "NOT NULL");

            // Default
            tmp = column.DefaultValue;
            if (!string.IsNullOrWhiteSpace(tmp) && "NULL" != tmp.ToUpper())
            {
                sql.Append($" DEFAULT {tmp}");
            }

            // COMMENT
            if (hasComment)
            {
                tmp = column.ColumnDescription;
                if (!string.IsNullOrWhiteSpace(tmp))
                {
                    sql.Append($" COMMENT '{tmp}'");
                }
            }

            return sql.ToString();
        }
    }
}
