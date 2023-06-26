/*
 * @Descripttion: 工具集
 * @version: 1.1
 * @LastEditors: sakuya
 * @LastEditTime: 2021年7月20日10:58:41
 */

import CryptoJS from "crypto-js";

const tool = {};

/* localStorage */
tool.data = {
	set(table, settings) {
		var _set = JSON.stringify(settings);
		return localStorage.setItem(table, _set);
	},
	get(table) {
		var data = localStorage.getItem(table);
		try {
			data = JSON.parse(data);
		} catch (err) {
			return null;
		}
		return data;
	},
	remove(table) {
		return localStorage.removeItem(table);
	},
	clear() {
		return localStorage.clear();
	},
};

/*sessionStorage*/
tool.session = {
	set(table, settings) {
		var _set = JSON.stringify(settings);
		return sessionStorage.setItem(table, _set);
	},
	get(table) {
		var data = sessionStorage.getItem(table);
		try {
			data = JSON.parse(data);
		} catch (err) {
			return null;
		}
		return data;
	},
	remove(table) {
		return sessionStorage.removeItem(table);
	},
	clear() {
		return sessionStorage.clear();
	},
};

/*cookie*/
tool.cookie = {
	set(name, value, config = {}) {
		var cfg = {
			expires: null,
			path: null,
			domain: null,
			secure: false,
			httpOnly: false,
			...config,
		};
		var cookieStr = `${name}=${escape(value)}`;
		if (cfg.expires) {
			var exp = new Date();
			exp.setTime(exp.getTime() + parseInt(cfg.expires) * 1000);
			cookieStr += `;expires=${exp.toGMTString()}`;
		}
		if (cfg.path) {
			cookieStr += `;path=${cfg.path}`;
		}
		if (cfg.domain) {
			cookieStr += `;domain=${cfg.domain}`;
		}
		cookieStr += ';samesite=none';
		document.cookie = cookieStr;
	},
	get(name) {
		var arr = document.cookie.match(
			new RegExp("(^| )" + name + "=([^;]*)(;|$)")
		);
		if (arr != null) {
			return unescape(arr[2]);
		} else {
			return null;
		}
	},
	remove(name) {
		var exp = new Date();
		exp.setTime(exp.getTime() - 1);
		document.cookie = `${name}=;expires=${exp.toGMTString()}`;
	},
};

/* Fullscreen */
tool.screen = function (element) {
	var isFull = !!(
		document.webkitIsFullScreen ||
		document.mozFullScreen ||
		document.msFullscreenElement ||
		document.fullscreenElement
	);
	if (isFull) {
		if (document.exitFullscreen) {
			document.exitFullscreen();
		} else if (document.msExitFullscreen) {
			document.msExitFullscreen();
		} else if (document.mozCancelFullScreen) {
			document.mozCancelFullScreen();
		} else if (document.webkitExitFullscreen) {
			document.webkitExitFullscreen();
		}
	} else {
		if (element.requestFullscreen) {
			element.requestFullscreen();
		} else if (element.msRequestFullscreen) {
			element.msRequestFullscreen();
		} else if (element.mozRequestFullScreen) {
			element.mozRequestFullScreen();
		} else if (element.webkitRequestFullscreen) {
			element.webkitRequestFullscreen();
		}
	}
};

/* 复制对象 */
tool.objCopy = function (obj) {
	return JSON.parse(JSON.stringify(obj));
};

tool.dateTimeFormat = function (time) {
	if (!time) {
		return '';
	}

	var date = new Date(eval(time));
	var year = date.getFullYear();
	var month = date.getMonth() + 1;
	var day = date.getDate();
	var hour = date.getHours();
	var minute = date.getMinutes();
	var second = date.getSeconds();
	return `${tool.lpad(year, 4)}-${tool.lpad(month)}-${tool.lpad(day)} ${tool.lpad(hour)}:${tool.lpad(minute)}:${tool.lpad(second)}`;
};

tool.lpad = function (val, total = 2, str = '0') {
	return val.toString().padStart(total, str)
};

/* 日期格式化
 * yyyy:年
 * MM:月
 * dd:日
 * hh:12制小时
 * HH:24制小时
 * mm:分
 * ss:秒
 * q:季度
 * S:毫秒
 * E:星期
 */
tool.dateFormat = function (time, fmt = "yyyy-MM-dd hh:mm:ss") {
	var date = new Date(time);
	var y = date.getFullYear();
	var M = date.getMonth() + 1;
	var d = date.getDate();
	var h24 = date.getHours();
	var h12 = (h24 % 12);
	var m = date.getMinutes();
	var s = date.getSeconds();
	var q = Math.floor((date.getMonth() + 3) / 3);
	var S = date.getMilliseconds();
	var E = date.getDay();
	var o = {
		"M+": M, //月份
		"d+": d, //日
		"h+": h12,
		"H+": h24, //小时
		"m+": m, //分
		"s+": s, //秒
		"q+": q, //季度
		"S": S, //毫秒
	};
	const week = ["\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d"];
	if (/(y+)/.test(fmt)) {
		fmt = fmt.replace(
			RegExp.$1,
			(y + "").substr(4 - RegExp.$1.length)
		);
	}
	if (/(E+)/.test(fmt)) {
		fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[E]);
	}
	for (var k in o) {
		if (new RegExp("(" + k + ")").test(fmt)) {
			fmt = fmt.replace(
				RegExp.$1,
				RegExp.$1.length == 1
					? o[k]
					: ("00" + o[k]).substr(("" + o[k]).length)
			);
		}
	}
	return fmt;
};

/* 千分符 */
tool.groupSeparator = function (num) {
	num = num + "";
	if (!num.includes(".")) {
		num += ".";
	}
	return num
		.replace(/(\d)(?=(\d{3})+\.)/g, function ($0, $1) {
			return $1 + ",";
		})
		.replace(/\.$/, "");
};

tool.changeTree = function (data) {
	if (data.length > 0) {
		data.forEach((item) => {
			const parentId = item.parentId;
			if (parentId) {
				data.forEach((ele) => {
					if (ele.id === parentId) {
						let childArray = ele.children;
						if (!childArray) {
							childArray = [];
						}

						childArray.push(item);
						ele.children = childArray;
					}
				});
			}
		});
	}
	return data.filter((item) => item.parentId == "0");
};

tool.uuid = function (length = 32) {
	const num =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
	let str = "";
	for (let i = 0; i < length; i++) {
		str += num.charAt(Math.floor(Math.random() * num.length));
	}
	return str;
};

tool.objKeySort = function (arys) {
	var newkey = Object.keys(arys).sort();
	var newObj = {}; //创建一个新的对象，用于存放排好序的键值对
	for (var i = 0; i < newkey.length; i++) {
		//遍历newkey数组
		newObj[newkey[i]] = arys[newkey[i]];
		//向新创建的对象中按照排好的顺序依次增加键值对
	}
	let resStr = "";
	for (const key in newObj) {
		if (newObj[key]) {
			resStr += key + newObj[key];
		}
	}

	return resStr;
};
tool.stringToByte = function (str) {
	var len, c;
	len = str.length;
	var bytes = [];
	for (var i = 0; i < len; i++) {
		c = str.charCodeAt(i);
		if (c >= 0x010000 && c <= 0x10ffff) {
			bytes.push(((c >> 18) & 0x07) | 0xf0);
			bytes.push(((c >> 12) & 0x3f) | 0x80);
			bytes.push(((c >> 6) & 0x3f) | 0x80);
			bytes.push((c & 0x3f) | 0x80);
		} else if (c >= 0x000800 && c <= 0x00ffff) {
			bytes.push(((c >> 12) & 0x0f) | 0xe0);
			bytes.push(((c >> 6) & 0x3f) | 0x80);
			bytes.push((c & 0x3f) | 0x80);
		} else if (c >= 0x000080 && c <= 0x0007ff) {
			bytes.push(((c >> 6) & 0x1f) | 0xc0);
			bytes.push((c & 0x3f) | 0x80);
		} else {
			bytes.push(c & 0xff);
		}
	}
	return new Int8Array(bytes);
};

/* 计算文件大小 */
tool.fileSize = function (limit) {
	var size = "";
	if (limit < 0.1 * 1024) {
		//小于0.1KB，则转化成B
		size = limit.toFixed(2) + "B";
	} else if (limit < 0.1 * 1024 * 1024) {
		//小于0.1MB，则转化成KB
		size = (limit / 1024).toFixed(2) + "KB";
	} else if (limit < 0.1 * 1024 * 1024 * 1024) {
		//小于0.1GB，则转化成MB
		size = (limit / (1024 * 1024)).toFixed(2) + "MB";
	} else {
		//其他转化成GB
		size = (limit / (1024 * 1024 * 1024)).toFixed(2) + "GB";
	}

	var sizeStr = size + ""; //转成字符串
	var index = sizeStr.indexOf("."); //获取小数点处的索引
	var dou = sizeStr.substr(index + 1, 2); //获取小数点后两位的值
	if (dou == "00") {
		//判断后两位是否为00，如果是则删除00
		return sizeStr.substring(0, index) + sizeStr.substr(index + 3, 2);
	}
	return size;
};

/* 常用加解密 */
tool.crypto = {
	//MD5加密
	MD5(data) {
		return CryptoJS.MD5(data).toString();
	},
	SHA(data) {
		return CryptoJS.SHA256(data).toString();
	},
	//BASE64加解密
	BASE64: {
		encrypt(data) {
			return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(data));
		},
		decrypt(cipher) {
			return CryptoJS.enc.Base64.parse(cipher).toString(
				CryptoJS.enc.Utf8
			);
		},
	},
	AES_SECRETKEY: "dd0308a654ea42eab695bf060241b5aa",
	//AES加解密
	AES: {
		encrypt(data, secretKey) {
			const result = CryptoJS.AES.encrypt(
				data,
				CryptoJS.enc.Utf8.parse(secretKey),
				{
					mode: CryptoJS.mode.ECB,
					padding: CryptoJS.pad.Pkcs7,
				}
			);
			return result.toString();
		},
		decrypt(cipher, secretKey) {
			const result = CryptoJS.AES.decrypt(
				cipher,
				CryptoJS.enc.Utf8.parse(secretKey),
				{
					mode: CryptoJS.mode.ECB,
					padding: CryptoJS.pad.Pkcs7,
				}
			);
			return CryptoJS.enc.Utf8.stringify(result);
		},
	},
};

tool.randomString = function (length, upper) {
	let result = '';
	const characters = upper ? '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ' : '0123456789abcdefghijklmnopqrstuvwxyz';
	const charactersLength = characters.length;

	for (let i = 0; i < length; i++) {
		result += characters.charAt(Math.floor(Math.random() * charactersLength));
	}

	return result;
};

export default tool;
