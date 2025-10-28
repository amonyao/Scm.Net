const DEFAULT_CONFIG = {
	// 应用代码
	APP_CODE: process.env.VUE_APP_CODE,

	// 应用名称
	APP_NAME: process.env.VUE_APP_NAME,

	//简介（支持HTML）
	APP_DESC: process.env.VUE_APP_DESC,

	//应用ID
	APP_KEY: "870cb6cfa286ac4cac462bc8e33b1bb5",

	//参数加密签名Key
	SIGN_KEY: "ab517f95fab7d57",

	//首页地址
	DASHBOARD_URL: "/dashboard",

	//版本号
	APP_VER: process.env.VUE_APP_VER,

	//内核版本号
	CORE_VER: process.env.VUE_APP_VER,

	// 构建日期
	APP_BUILD: process.env.VUE_APP_BUILD,

	//接口地址
	API_URL: process.env.VUE_APP_API_BASEURL + "/api",

	//服务器地址
	SERVER_URL: process.env.VUE_APP_API_BASEURL,

	//SignalR地址
	SignalR_URL: process.env.VUE_APP_API_BASEURL + "/scmhub",

	//请求超时
	TIMEOUT: 10000,

	//TokenName
	TOKEN_NAME: "accessToken",

	//Token前缀，注意最后有个空格，如不需要需设置空字符串
	TOKEN_PREFIX: "",

	//追加其他头
	HEADERS: {},

	//请求是否开启缓存
	REQUEST_CACHE: false,

	//布局 默认：default | 通栏：header | 经典：menu | 功能坞：dock
	//dock将关闭标签和面包屑栏
	LAYOUT: "default",

	//菜单是否折叠
	MENU_IS_COLLAPSE: false,

	//菜单是否启用手风琴效果
	MENU_UNIQUE_OPENED: false,

	//是否开启多标签
	LAYOUT_TAGS: true,

	//语言
	LANG: "zh-cn",

	//主题颜色
	COLOR: "",

	//首面
	HOME: "1",

	//控制台首页默认布局
	DEFAULT_GRID: {
		//默认分栏数量和宽度 例如 [24] [18,6] [8,8,8] [6,12,6]
		layout: [12, 6, 6],
		//小组件分布，com取值:views/home/components 文件名
		copmsList: [["welcome", "ver"], ["time", "progress"], ["about"]],
	},

	DEF_LOGIN_TYPE: 2,
	// 默认登录配置
	DEF_LOGIN_MODE: [10, 40],
	// 默认登录单位
	DEF_LOGIN_UNIT: "dev",
	// 默认登录用户名
	DEF_LOGIN_USER: "",
	// 默认登录密码
	DEF_LOGIN_PASS: "",

	// OIDC配置
	OIDC_OSP: process.env.VUE_APP_OIDC_OSP,
	OIDC_KEY: process.env.VUE_APP_OIDC_KEY,
	OIDC_LOGO: process.env.VUE_APP_OIDC_LOGO,
	OIDC_AUTH: process.env.VUE_APP_OIDC_AUTH,
	OIDC_BIND: process.env.VUE_APP_OIDC_BIND,
	OIDC_LOGIN: "http://www.oidc.org.cn/web/login?client_id={key}&state={state}",
	OIDC_REDIRECT_URI: "http://www.c-scm.net/#/oauth",

	PREDEFINE_COLORS: [
		"#ffffff",
		"#cccccc",
		"#999999",
		"#666666",
		"#333333",
		"#000000",
		"#ff0000",
		"#ff4500",
		"#ff8c00",
		"#ffd700",
		"#00ff00",
		"#90ee90",
		"#67c23a",
		"#00ced1",
		"#009688",
		"#1e90ff",
		"#409eff",
		"#536dfe",
		"#0000ff",
		"#c71585",
	],

	BEIAN_MIIT: "",
	BEIAN_MPS: "",
};

//合并业务配置
// import MY_CONFIG from "./myConfig";
// Object.assign(DEFAULT_CONFIG, MY_CONFIG);

// 如果生产模式，就合并动态的APP_CONFIG
// public/config.js
// if (process.env.NODE_ENV === "production") {
// 	Object.assign(DEFAULT_CONFIG, APP_CONFIG);
// }
Object.assign(DEFAULT_CONFIG, APP_CONFIG);

export default DEFAULT_CONFIG;
