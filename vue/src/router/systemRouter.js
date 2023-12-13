import config from "@/config";

//系统路由
const routes = [
	{
		name: "layout",
		path: "/",
		component: () => import(/* webpackChunkName: "layout" */ "@/layout"),
		redirect: config.DASHBOARD_URL || "/dashboard",
		children: [],
	},
	{
		path: "/about",
		component: () => import(/* webpackChunkName: "login" */ "@/views/about"),
		meta: { title: "关于" },
	},
	{
		path: "/about_site",
		component: () => import(/* webpackChunkName: "login" */ "@/views/about/site"),
		meta: { title: "关于网站" },
	},
	{
		path: "/about_author",
		component: () => import(/* webpackChunkName: "login" */ "@/views/about/author"),
		meta: { title: "关于作者" },
	},
	{
		path: "/login",
		component: () => import(/* webpackChunkName: "login" */ "@/views/login"),
		meta: { title: "登录" },
	},
	{
		path: "/user_register",
		component: () => import(/* webpackChunkName: "userRegister" */ "@/views/login/userRegister"),
		meta: { title: "用户注册" },
	},
	{
		path: "/reset_password",
		component: () => import(/* webpackChunkName: "resetPassword" */ "@/views/login/resetPassword"),
		meta: { title: "重置密码" },
	},
];

export default routes;
