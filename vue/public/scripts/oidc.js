var oidc = (function () {
	// OIDC版本
	var _ver = 1.1;
	// OIDC名称
	var _name = "OIDC";
	// OIDC前缀
	var _pre = "_oidc_";
	// OIDC网址
	var _base = "http://www.oidc.org.cn";
	// OIDC应用KEY
	var _key = '';
	// OIDC初始化选项
	var _option = {};
	// 是否输出日志
	var _log = false;
	// 是否调试模式
	var _debug = false;
	// 根对象
	var _root;
	// 对话框
	var _dialog;

	function $(div) {
		return document.getElementById(div);
	}

	function _(child, element) {
		var obj = document.createElement(child);
		if (element) {
			element.appendChild(obj);
		}
		return obj;
	}

	function log(txt) {
		if (!_log) {
			return;
		}
		console.log('[OIDC]' + txt);
	}

	function text(element, text) {
		element.innerText = text;
	}

	function html(element, html) {
		element.innerHTML = html;
	}

	function css(element, propertyName, value) {
		// 确保 element 是一个 DOM 元素
		if (!(element instanceof HTMLElement)) {
			throw new Error("The first argument must be a DOM element.");
		}

		// 如果提供了 value，则设置样式
		if (value !== undefined) {
			// 使用 camelCase 来设置样式属性（例如，'background-color' 变为 'backgroundColor'）
			var styleName = propertyName.replace(/-(\w)/g, function (match, char) {
				return char.toUpperCase();
			});

			// 设置样式
			element.style[styleName] = value;
		}
		// 如果没有提供 value，则返回样式的当前值
		else {
			// 尝试获取 camelCase 风格的样式值
			var computedStyle = window.getComputedStyle(element);
			var styleValue = computedStyle.getPropertyValue(propertyName);

			// 注意：有些样式可能是空字符串，因为它们可能没有被明确设置
			return styleValue;
		}
	}

	function att(element, propertyName, value) {
		if (!(element instanceof HTMLElement)) {
			throw new Error("The first argument must be a DOM element.");
		}

		if (value != undefined) {
			element.setAttribute(propertyName, value);
		} else {
			return element.getAttribute(propertyName);
		}
	}

	// 1. hasClass 是否包含某个class，可以直接用 dom.classList.contains
	function hasClass(curEle, className) {
		var reg = new RegExp("(^|\\s)" + className + "(\\s|$)");
		// var reg = new RegExp('(^| +)' + className + '( +|$)')
		return reg.test(curEle.className);
	}

	// 2. addClass支持传多个class，以空格隔开，可以直接用 dom.classList.add(class,class,...)，不过存在兼容问题
	function addClass(curEle, className) {
		var ary = className.replace(/(^ +| +$)/g, "").split(/ +/g); // split(' ')

		for (var i = 0; i < ary.length; i++) {
			var curClass = ary[i];
			if (!hasClass(curEle, curClass)) {
				curEle.className += " " + curClass;
			}
		}
	}

	// 3. removeClass，支持传多个class，以空格隔开，可以直接用 dom.classList.remove(class, class, ...)，不过存在兼容问题
	function removeClass(curEle, className) {
		var ary = className.replace(/(^ +| +$)/g, "").split(/ +/g);

		for (var i = 0; i < ary.length; i++) {
			var curClass = ary[i];
			if (hasClass(curEle, curClass)) {
				var reg = new RegExp("(^| +)" + curClass + "( +|$)", "g");
				curEle.className = curEle.className.replace(reg, " ").trim();
			}
		}
	}

	function isMore(code) {
		return code == 'oidc' || code == 'More';
	}

	function asList(oidc_list) {
		var text = "";
		for (var i = 0; i < oidc_list.length; i += 1) {
			var item = oidc_list[i];
			var tips = !isMore(item.code) ? '使用 ' + item.name + ' 登录' : '显示更多';
			text += '<div class="oidc-list-item" title="' + tips + '" onclick="oidc.toUri(\'' + item.code + '\')">';
			text += '<span class="logo"><img src="' + _base + '/data/logo/' + item.icon + '" alt="' + item.code + '" /></span>';
			text += '<span class="line"></span>';
			text += '<span class="text">' + item.name + '</span>';
			text += '</div>';
		}
		return text;
	}

	function asIcon(oidc_list) {
		var text = "";
		for (var i = 0; i < oidc_list.length; i += 1) {
			var item = oidc_list[i];
			var tips = !isMore(item.code) ? '使用 ' + item.name + ' 登录' : '显示更多';
			text += '<div class="oidc-list-icon" title="' + tips + '" onclick="oidc.toUri(\'' + item.code + '\')">';
			text += '<span class="logo"><img src="' + _base + '/data/logo/' + item.icon + '" alt="' + item.code + '" /></span>';
			text += '</div>';
		}
		return text;
	}

	function asCard(oidc_list) {
		var text = "";
		for (var i = 0; i < oidc_list.length; i += 1) {
			var item = oidc_list[i];
			var tips = !isMore(item.code) ? '使用 ' + item.name + ' 登录' : '显示更多';
			text += '<div class="oidc-list-card" title="' + tips + '" onclick="oidc.toUri(\'' + item.code + '\')">';
			text += '<div class="logo"><img src="' + _base + '/data/logo/' + item.icon + '" alt="' + item.code + '" /></div>';
			text += '<div class="text">' + item.name + '</div>';
			text += '</div>';
		}
		return text;
	}

	function showDialog() {
		var dialog = _('div', document.body);
		dialog.addEventListener("mousedown", () => {
			dialog.addEventListener("mousemove", moveDialog);
		});
		document.addEventListener("mouseup", () => {
			dialog.removeEventListener("mousemove", moveDialog);
		});
	}

	function startDrag(event) {
		event.preventDefault();

		document.addEventListener("mousemove", moveDialog);
		document.addEventListener("mouseup", stopDrag);
	}

	function stopDrag(event) {
		document.removeEventListener("mousemove", moveDialog);
		document.removeEventListener("mouseup", stopDrag);
	}

	function moveDialog({ movementX, movementY }) {
		let getContainerStyle = window.getComputedStyle(_dialog);
		let leftValue = parseInt(getContainerStyle.left);
		let topValue = parseInt(getContainerStyle.top);
		_dialog.style.left = `${leftValue + movementX}px`;
		_dialog.style.top = `${topValue + movementY}px`;
	}

	// 返回变量
	return {
		/**
		 * 当前版本
		 */
		ver: _ver,

		/**
		 * 初始化
		 * @param {string} container 容器ID
		 * @param {object} option 初始化参数
		 * {
		 *  'showHead':boolean
		 *  'headHtml':string
		 *  'showFoot':boolean
		 *  'footHtml':string
		 *  'appKey':string
		 *  'appState':string
		 * }
		 * @returns
		 */
		init: function (container, option) {
			if (!option) {
				option = {};
			}
			if (!option.response_type) {
				option.response_type = 'code';
			}
			_option = option;

			_log = !!option.log;
			log('ver:' + _ver);

			_debug = !!option.debug;

			if (!container) {
				container = "oidc";
			}
			_root = $(container);
			if (!_root) {
				return;
			}
			addClass(_root, "oidc");

			if (!oidc_data) {
				oidc_data = {};
			}
			_key = oidc_data.app_key || '';
			var oidc_list = oidc_data.oidc_list;
			if (!oidc_list || !oidc_list.length) {
				oidc_list = [];
			}

			var showCard = option.showCard;
			if (showCard == undefined) {
				showCard = true;
			}

			var oidcDiv = _root;
			if (showCard) {
				var oidcCard = _("div", _root);
				addClass(oidcCard, "oidc-card");
				oidcDiv = oidcCard;
			}

			var showHead = !!option.showHead;
			if (showCard && showHead) {
				var oidcHead = _("div", oidcDiv);
				addClass(oidcHead, "oidc-card_head");
				html(oidcHead, option.headHtml);
			}

			var oidcBody = oidcDiv;
			if (showCard) {
				oidcBody = _("div", oidcDiv);
				oidcBody = _("div", oidcDiv);
				addClass(oidcBody, "oidc-card_body");
			}

			var oidcList = _("div", oidcBody);
			addClass(oidcList, "oidc-list");

			var view = (option.view || 'item').toLowerCase();
			log('view:' + view);

			var text = '';
			if (view == 'icon') {
				css(oidcList, "display", "flex");
				css(oidcList, "flex-direction", "row");
				text = asIcon(oidc_list);
			}
			else if (view == 'card') {
				css(oidcList, "display", "grid");
				var cols = option.columns || 1;
				if (cols < 1) {
					cols = 1;
				}
				if (cols > 1) {
					css(oidcList, "grid-template-columns", "repeat(" + cols + ", " + 100 / cols + "%)");
				}
				text = asCard(oidc_list);
			}
			else {
				text = asList(oidc_list);
			}
			html(oidcList, text);

			var showFoot = !!option.showFoot;
			if (showCard && showFoot) {
				var oidcFoot = _("div", oidcDiv);
				addClass(oidcFoot, "oidc-card_foot");
				html(oidcFoot, option.footHtml);
			}
		},

		/**
		 * 显示对话框
		 */
		showDialog: function () {
			if (_dialog) {
				return;
			}

			_dialog = _('div', _root);
			addClass(_dialog, 'oidc-dialog');

			var head = _('div', _dialog);
			addClass(head, 'oidc-dialog-head');

			var title = _('div', head);
			addClass(title, 'title');
			html(title, 'OIDC');

			var close = _('div', head);
			addClass(close, 'close');
			close.addEventListener('', () => {
				oidc.hideDialog();
			});
			text(close, 'X');

			var body = _('div', _dialog);
			addClass(body, 'oidc-dialog-body');
			html(body, '<iframe src="http://localhost:7201/oauth/dialog"></iframe>');

			var foot = _('div', _dialog);
			addClass(foot, 'oidc-dialog-foot');
			html(foot, 'Powered By OIDC');

			title.addEventListener("mousedown", startDrag);
		},

		hideDialog: function () {
			css(_dialog, 'display', 'hidden');
		},

		/**
		 * 跑转到指定地址 
		 */
		toUri: function (code) {
			code = code.toLowerCase();
			var url = _base + "/oauth";
			if (code == "more") {
				url += "/index";
			} else if (code == "phone") {
				url += "/phone";
			} else if (code == "email") {
				url += "/email";
			} else {
				url += "/login/" + code;
			}

			var tmp = "";
			if (_key) {
				tmp += "&client_id=" + _key;
			}
			//if (_option.response_type) {
			//	tmp += "&response_type=" + _option.response_type;
			//}
			//if (_option.redirect_uri) {
			//	tmp += "&redirect_uri=" + _option.redirect_uri;
			//}
			if (_option.state) {
				tmp += "&state=" + _option.state;
			}
			//if (_option.scope) {
			//	tmp += "&scope=" + _option.scope;
			//}
			if (tmp.length > 0) {
				url += "?" + tmp.substring(1);
			}

			log('url:' + url);
			if (!_debug) {
				window.location.href = url;
			}
		},
	};
})();
