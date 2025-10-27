﻿using Com.Scm.Dto;
using Com.Scm.Enums;

namespace Com.Scm.Adm.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class AdmConfigCatDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string codec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string namec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int od { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ScmSystemEnum row_system { get; set; }
    }
}
