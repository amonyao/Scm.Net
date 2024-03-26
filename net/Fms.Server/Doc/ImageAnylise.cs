using Com.Scm.Cms.Res;
using Com.Scm.Config;
using Com.Scm.Enums;
using Com.Scm.Image.SkiaSharp;
using SqlSugar;

namespace Com.Scm.Cms.Doc
{
    public class ImageAnylise
    {
        public ISqlSugarClient _Client;
        private EnvConfig _EnvConfig;

        public ImageAnylise(ISqlSugarClient client, EnvConfig envConfig)
        {
            _Client = client;
            _EnvConfig = envConfig;
        }

        public void Run()
        {
            var handleListDao = _Client.Queryable<FileHandleDao>()
                .Where(a => a.handle == ScmHandleEnum.Todo)
                .ToList();

            foreach (var handleDao in handleListDao)
            {
                Run(handleDao);
            }
        }

        public void Run(FileHandleDao handleDao)
        {
            if (ChangeHandle(handleDao, ScmHandleEnum.Todo, ScmHandleEnum.Doing) < 1)
            {
                return;
            }

            var fileDao = _Client.Queryable<FileDao>()
                .Where(a => a.id == handleDao.id)
                .First();
            if (fileDao == null)
            {
                ChangeHandle(handleDao, ScmHandleEnum.Doing, ScmHandleEnum.Failure);
                return;
            }

            var engine = new ImageEngine();
            var path = _EnvConfig.GetUploadPath(fileDao.path);
            if (!File.Exists(path))
            {
                return;
            }
            var image = engine.Load(path);
            if (image == null)
            {
                ChangeHandle(handleDao, ScmHandleEnum.Doing, ScmHandleEnum.Failure);
                return;
            }

            var imageDao = new ImageDao();
            imageDao.width = image.Width;
            imageDao.height = image.Height;
            imageDao.id = fileDao.id;
            imageDao.unit_id = fileDao.unit_id;
            _Client.Insertable(imageDao).ExecuteCommand();

            var colors = image.GetImageColor(10);
            if (colors != null)
            {
                var list = new List<ImageColorDao>();
                foreach (var color in colors)
                {
                    var colorDao = _Client.Queryable<ColorDao>()
                        .Where(a => a.color == color.Color)
                        .First();
                    if (colorDao == null)
                    {
                        colorDao = new ColorDao();
                        colorDao.color = color.Color;
                        colorDao.qty = 1;
                        _Client.Insertable(colorDao).ExecuteCommand();
                    }
                    else
                    {
                        colorDao.qty += 1;
                        _Client.Updateable(colorDao).ExecuteCommand();
                    }

                    var imageColorDao = new ImageColorDao();
                    imageColorDao.file_id = fileDao.id;
                    imageColorDao.od = list.Count;
                    imageColorDao.color_id = colorDao.id;
                    list.Add(imageColorDao);
                }
                _Client.Insertable(list).ExecuteCommand();
            }

            ChangeHandle(handleDao, ScmHandleEnum.Doing, ScmHandleEnum.Done);
        }

        private int ChangeHandle(FileHandleDao handleDao, ScmHandleEnum last, ScmHandleEnum next)
        {
            handleDao.handle = next;

            var qty = _Client.Updateable(handleDao)
                .Where(a => a.id == handleDao.id && a.handle == last)
                .ExecuteCommand();
            return qty;
        }
    }
}
