﻿【项目说明】
此项目主要用于表达〖数据传输对象〗（Data Transfer Object）映射类，负责前后端的数据传输。
当有多个项目需要使用相同的DAO时，可以引用此项目。

【注意事项】
1、此模块可以根据需要单独存在，或者与Server项目整合；
2、DTO对象属性可以与DAO对象属性不一致，以避免暴露后台数据库信息；
3、DTO更专注于页面与后端服务的数据交互，不一定会有数据库的存储；

【项目依赖】
1、Scm.Common：框架提供的一些公共基础方法；
2、Scm.Common.Dto：Dto基类相关。