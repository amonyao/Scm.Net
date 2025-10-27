﻿using Com.Scm.Dto;
using Com.Scm.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Dev
{
    public class DevVerDetailDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long ver_id { get; set; }

        /// <summary>
        /// 升级类型
        /// </summary>
        public ScmVerDetailTypesEnum types { get; set; }

        /// <summary>
        /// 升级事项
        /// </summary>
        [StringLength(256)]
        public string content { get; set; }
    }
}
