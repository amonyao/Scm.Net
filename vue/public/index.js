function getBrowerInfo() {
	var userAgent = window.navigator.userAgent;
	var browerInfo = {
		type: "unknown",
		version: "unknown",
		userAgent: userAgent,
	};
	if (document.documentMode) {
		browerInfo.type = "IE";
		browerInfo.version = document.documentMode + "";
	} else if (indexOf(userAgent, "Firefox")) {
		browerInfo.type = "Firefox";
		browerInfo.version = userAgent.match(/Firefox\/([\d.]+)/)[1];
	} else if (indexOf(userAgent, "Opera")) {
		browerInfo.type = "Opera";
		browerInfo.version = userAgent.match(/Opera\/([\d.]+)/)[1];
	} else if (indexOf(userAgent, "Edg")) {
		browerInfo.type = "Edg";
		browerInfo.version = userAgent.match(/Edg\/([\d.]+)/)[1];
	} else if (indexOf(userAgent, "Chrome")) {
		browerInfo.type = "Chrome";
		browerInfo.version = userAgent.match(/Chrome\/([\d.]+)/)[1];
	} else if (indexOf(userAgent, "Safari")) {
		browerInfo.type = "Safari";
		browerInfo.version = userAgent.match(/Safari\/([\d.]+)/)[1];
	}
	return browerInfo;
}
function indexOf(userAgent, brower) {
	return userAgent.indexOf(brower) > -1;
}
function $(id) {
	return document.getElementById(id);
}
function checkBrower() {
	var minVer = {
		Chrome: 71,
		Firefox: 65,
		Safari: 12,
		Edg: 97,
		IE: 999,
	};
	var browerInfo = getBrowerInfo();
	var materVer = browerInfo.version.split(".")[0];
	if (eval(materVer) < minVer[browerInfo.type]) {
		$("versionCheck").style.display = "block";
		$("versionCheck-type").innerHTML = browerInfo.type;
		$("versionCheck-version").innerHTML = browerInfo.version;
	}
}
checkBrower();
