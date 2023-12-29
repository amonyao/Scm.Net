<template>
    <el-container>
        <el-aside width="260px" v-loading="menuloading">
            <el-container>
                <el-header>
                    <sc-select v-model="param.db_id" :data="db_list" @change="changeDb"></sc-select>
                </el-header>
                <el-main style="background-color: #FFFFFF;">
                    <div class="filter">
                        <el-input v-model="menuFilter" clearable placeholder="输入关键字进行过滤" />
                        <el-button type="primary" @click="search"><sc-icon icon="sc-search"/></el-button>
                    </div>
                    <el-tabs>
                        <el-tab-pane label="表结构">
                            <el-scrollbar>
                                <div class="tree-container">
                                    <div v-for="(table, index) in treeData" :key="table.table" class="tree-table">
                                        <div class="tree-table-name" @click="onFoldTable(index)">
                                            <span class="tree-fold-btn"
                                                :class="['tree-fold-btn', foldIndex.includes(index) ? 'el-icon-remove-outline' : 'el-icon-circle-plus-outline']" />
                                            <img src="@/assets/table.png" width="15px">
                                            {{ table.table }}
                                        </div>
                                        <template v-if="table.fields.length > 0 && foldIndex.includes(index)">
                                            <div v-for="field in table.fields" :key="field.name" class="tree-table-field">
                                                {{ field.COLUMN_NAME }}
                                                <!-- <span class="el-icon-document-copy"></span> -->
                                            </div>
                                        </template>
                                    </div>
                                </div>
                            </el-scrollbar>
                        </el-tab-pane>
                        <el-tab-pane label="命令集">
                            <el-scrollbar>
                                <sc-list :data="presql_list" @change="changeItem" @editItem="editItem"
                                    @dropItem="deleteItem">
                                    <template #item="{ item }">
                                        {{ item.namec }}
                                    </template>
                                </sc-list>
                            </el-scrollbar>
                        </el-tab-pane>
                    </el-tabs>
                </el-main>
            </el-container>
        </el-aside>
        <el-main class="nopadding">
            <el-container>
                <el-header>
                    <div class="left-panel">
                        <el-button-group>
                            <el-button type="primary" plain icon="el-icon-plus" @click="runCode()">运行</el-button>
                            <el-button type="primary" plain icon="el-icon-delete" @click="stopRun()">停止</el-button>
                            <el-button type="danger" plain icon="el-icon-delete" @click="commit()">提交</el-button>
                        </el-button-group>
                        <el-divider direction="vertical"></el-divider>
                        <el-button type="success" plain icon="el-icon-delete" @click="format()">美化</el-button>
                        <el-divider direction="vertical"></el-divider>
                        <el-button type="default" plain icon="el-icon-delete" @click="saveAs()">保存</el-button>
                    </div>
                    <div class="right-panel">
                        <div class="right-panel-search">
                        </div>
                    </div>
                </el-header>
                <el-main class="nopadding">
                    <div ref="sqlEditor" class="sqlEditor"></div>
                    <div class="tables-result" :style="{ height: `${tablesHeight}px` }">
                        <el-tabs type="border-card" style="height: 99%">
                            <el-tab-pane label="信息">
                                <el-icon>
                                    <ScIconInfoFill color="#909399" v-if="runType === 0" />
                                    <ScIconSuccessFill color="#67C23A" v-if="runType === 1" />
                                    <ScIconWarningFill color="#F56C6C" v-if="runType === 2" />
                                    <ScIconErrorFill color="#F56C6C" v-if="runType === 3" />
                                </el-icon>
                                {{ runResult }}
                                <template v-if="runType === 3">
                                    <div class="error-message"><b>脚本：</b>"{{ errMsg.sql }}"</div>
                                    <div class="error-message"><b>异常：</b>{{ errMsg.message }}</div>
                                </template>
                            </el-tab-pane>
                            <el-tab-pane label="结果">
                                <el-scrollbar>
                                    <el-table :data="tableData" border>
                                        <el-table-column v-for="item in columns" :key="item.value" :prop="item.value"
                                            :label="item.label" :show-overflow-tooltip="true" :minWidth="item.minWidth"
                                            :width="item.width" />
                                    </el-table>
                                </el-scrollbar>
                            </el-tab-pane>
                        </el-tabs>
                    </div>
                </el-main>
            </el-container>
        </el-main>
    </el-container>
    <edit ref="edit" @complete="complete" />
</template>
  
<script>
import { defineAsyncComponent } from "vue";
import { EditorView, basicSetup } from "codemirror";
import { EditorState } from "@codemirror/state";
import { javascript } from "@codemirror/lang-javascript";
import { sql } from "@codemirror/lang-sql";
import { vkbeautify } from "vkbeautify";

export default {
    name: 'SQL',
    components: {
        edit: defineAsyncComponent(() => import("./edit")),
    },
    data() {
        return {
            menuloading: false,//列表加载标记
            menuFilter: '',//列表过滤条件
            tablesHeight: 0,
            foldIndex: [], // 记录展开状态的表结构，存入index
            treeData: [], // 左侧展示的表结构
            db_list: [],//数据库列表
            table_list: [],// 所有表格信息
            presql_list: [],//预定义脚本
            runType: 0, // 运行结果类型，0无结果、1成功、2警告、3异常
            runResult: '暂无数据', // 运行结果提示
            tableData: [], // 运行结果的表数据展示
            columns: [], // 运行的表结构，对象元素，{label: '', value: ''}
            sqlEditor: null,
            param: {
                db_id: '0'
            },
            options: {
                theme: '',
                // lineNumbers: true,
                styleActiveLine: true,
                lineWrapping: true,
            },
            code: '', // 实时输入的代码
            runLoading: false, // 运行加载状态
            errMsg: {
                sql: '',
                message: '',
                errno: '',
                code: '',
                sqlState: ''
            },
        }
    },
    mounted() {
        this.$SCM.list_option(this.db_list, this.$API.devdb.option, {}, false);
        this.init();
    },
    methods: {
        init() {
            let startState = EditorState.create({
                extensions: [basicSetup, javascript(), sql()]
            })

            this.sqlEditor = new EditorView({
                state: startState,
                parent: this.$refs.sqlEditor
            })
        },
        getValue() {
            return this.sqlEditor.state.doc.toString();
        },
        getLine() {
            return '';//this.sqlEditor.state.doc.line(n + 1).text;
        },
        getCursor() {
            return this.sqlEditor.state.selection.main.head;
        },
        getSelection() {
            var selection = this.sqlEditor.state.selection.main;
            return this.sqlEditor.state.sliceDoc(selection.from, selection.to);
        },
        setValue(value) {
            this.sqlEditor.dispatch({ changes: { from: 0, to: this.sqlEditor.state.doc.length, insert: value } });
        },
        changeDb() {
            this.queryTables();
            this.queryPresql();
        },
        complete() {
            this.queryPresql();
        },
        onFoldTable(index) {
            // 表结构折叠事件
            const i = this.foldIndex.indexOf(index)
            if (i === -1) {
                this.foldIndex.push(index)
            } else {
                this.foldIndex.splice(i, 1)
            }
        },
        search() {
            if (!this.table_list) {
                return;
            }
            this.treeData = []
            const tips = {}
            this.table_list.forEach(async (table) => {
                if (table.name && table.name.indexOf(this.menuFilter) >= 0) {
                    const treeObj = {
                        table: table.name,
                        fields: []
                    }
                    tips[table.name] = []

                    // 查询表中的字段
                    // const fieldRes = await this.$API.devsql.column.get({ 'table': table.name });
                    // if (fieldRes != null && fieldRes.code == 200) {
                    //     treeObj.fields = [...fieldRes.data]
                    //     fieldRes.data.forEach(f => {
                    //         tips[table.name].push(f.COLUMN_NAME)
                    //     });
                    // }

                    this.treeData.push(treeObj)
                }
            })
            //this.setHintOptions(tips)
        },
        format() {
            var code = this.getValue().trim();
            // 格式化代码
            if (code === '') {
                this.$message.warning('请先编辑 SQL 命令！')
                return
            }
            this.code = code;
            this.setValue(vkbeautify.sql(code));
        },
        saveAs() {
            var code = this.getCode();
            if (!code) {
                this.$message.warning('请先编辑 SQL 命令！')
                return;
            }

            this.$refs.edit.open({ db_id: this.param.db_id, sql: code });
        },
        getCode() {
            var code = this.getSelection().trim();
            if (code) {
                return code;
            }

            code = this.getLine().trim();
            if (code) {
                return code;
            }

            return this.getValue().trim();
        },
        runCode() {
            var code = this.getCode();
            if (!code) {
                this.$message.warning('请先编辑 SQL 命令！')
                return;
            }

            this.executeSql(code);
        },
        stopRun() {

        },
        commit() {
            // SQL编辑器提交事件
            var code = this.$TOOL.trim(this.code || '');
            if (code === '') {
                this.$message.warning('请先编辑 SQL 命令！')
                return
            }
            this.$prompt('请输入命令标题：', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                inputValidator: (val) => {
                    if (val.trim() === '') {
                        return false
                    }
                    return true
                },
                inputErrorMessage: '命令标题不能为空'
            }).then(({ value }) => {
                if (value.trim() !== '') {
                    const logs = {
                        title: value,
                        sql: code,
                        createTime: Date.now()
                    }
                    this.addLogs(logs)
                    this.$message.success('提交成功！')
                } else {
                    this.$message.error('提交失败！')
                }
            }).catch(() => {
                this.$message.warning('已取消提交')
            })
        },
        // setHintOptions(tables) {
        // this.sqlEditor.options.hintOptions.tables = tables
        // },
        editorEvents() {
            // 设置代码提示
            // this.sqlEditor.on('keyup', (cm, event) => {
            //     if (event.keyCode >= 65 && event.keyCode <= 90) {
            //         cm.showHint();
            //     }
            //     //所有的字母和'$','{','.'在键按下之后都将触发自动完成  
            //     if (!cm.state.completionActive &&
            //         ((event.keyCode >= 65 && event.keyCode <= 90) || event.keyCode == 52 || event.keyCode == 219 || event.keyCode == 190)) {
            //         CodeMirror.commands.autocomplete(cm, null, { completeSingle: false });
            //     }
            // })

            // // 代码输入的双向绑定
            // this.sqlEditor.on('change', (sqlEditor) => {
            //     // 这里要用多一个载体去获取值,不然会重复赋值卡顿
            //     this.code = sqlEditor.getValue()
            //     if (this.$emit) {
            //         this.$emit('input', this.code)
            //     }
            // })
        },
        copySQLOrder(item) {
            // 复制命令
            this.setValue(item.sql)
        },
        async executeSql(code) {
            // 执行SQL语句
            code = this.$TOOL.trim(code || '');
            if (code === '') {
                this.$message.warning('请先编辑 SQL 命令！');
                return
            }
            this.runLoading = true
            this.tableData = []
            this.columns = []
            this.runResult = '执行中...'
            this.runType = 0
            this.errMsg = {}
            const res = await this.$API.devsql.execute.post({ db_id: this.param.db_id, sql: code });
            if (res == null || res.code != 200) {
                this.$message.warning('服务器响应异常，请稍后重试！');
                return;
            }

            var data = res.data;
            if (data.success) {
                if (data.type == 1) {
                    this.runType = 1
                    this.runResult = `成功查询 ${data.totalItems} 条数据`
                    if (data.data.length > 0) {
                        this.tableData = [...data.data];
                        const obj = { ...this.tableData[0] }
                        for (let key in obj) {
                            this.columns.push({
                                label: key,
                                value: key,
                                width: this.get_width(key),
                                minWidth: this.get_width(key)
                            })
                        }
                    }
                }
                else {
                    this.runType = 1
                    this.runResult = `执行成功！${data.qty}行数据受影响`
                    this.queryTables();
                }
            } else {
                this.runType = 3
                this.runResult = '执行失败！'
                this.errMsg.message = data.message;
                this.errMsg.sql = code;
            }
            this.runLoading = false
        },
        get_width(key) {
            return key.length * 8 + 60;
        },
        async queryTables() {
            this.menuloading = true;
            // 查询所有表
            const res = await this.$API.devsql.table.get(this.param);
            if (res.code != 200) {
                this.menuloading = false;
                return;
            }

            this.table_list = res.data;
            const data = res.data;
            if (data != null && data.length > 0) {
                this.treeData = []
                const tips = {}
                data.forEach(async (table) => {
                    if (table.name && table.name.trim() !== '') {
                        const treeObj = {
                            table: table.name,
                            fields: []
                        }
                        tips[table.name] = []

                        // 查询表中的字段
                        // const fieldRes = await this.$API.devsql.column.get({ 'table': table.name });
                        // if (fieldRes != null && fieldRes.code == 200) {
                        //     treeObj.fields = [...fieldRes.data]
                        //     fieldRes.data.forEach(f => {
                        //         tips[table.name].push(f.COLUMN_NAME)
                        //     });
                        // }

                        this.treeData.push(treeObj)
                    }
                })
                //this.setHintOptions(tips)
            }
            this.menuloading = false;
        },
        async queryPresql() {
            this.menuloading = true;
            // 查询所有表
            const res = await this.$API.devsql.presql.get(this.param);
            if (res.code != 200) {
                this.menuloading = false;
                return;
            }

            this.presql_list = res.data;
            this.menuloading = false;
        },
        changeItem(item) {
            this.setValue(item.sql);
        },
        editItem(item) {
            this.$refs.edit.open(item);
        },
        refresh() {

        },
        deleteItem(item) {
            this.$confirm(`确认要执行删除操作吗？`, "提示", {
                type: "warning",
                confirmButtonText: "确定",
                cancelButtonText: "取消",
            })
                .then(async () => {
                    var loading = this.$loading();
                    var res = this.$API.devsql.delete.delete({ ids: item.id });
                    if (res.code == 200) {
                        this.queryPresql();
                        loading.close();
                        this.$message.success("操作成功。");
                    } else {
                        this.$alert(res.message, "提示", { type: "error" });
                    }
                })
                .catch(() => { });
        }
    }
}
</script>
<style scoped>
.filter {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.sqlEditor {
    width: 100%;
    height: 360px;
}
</style>
<style>
.cm-editor {
    height: 100%;
    width: 100%;
}
</style>