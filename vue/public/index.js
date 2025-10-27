function $(id) {
	return document.getElementById(id);
}
function getInfo(browerInfo, userAgent, brower) {
	if (userAgent.indexOf(brower) > -1) {
		browerInfo.type = brower;
		browerInfo.version = userAgent.match(
			new RegExp(brower + "/([\\d.]+)")
		)[1];
		return true;
	}
	return false;
}
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
		return browerInfo;
	}
	if (getInfo(browerInfo, userAgent, "Firefox")) {
		return browerInfo;
	}
	if (getInfo(browerInfo, userAgent, "Opera")) {
		return browerInfo;
	}
	if (getInfo(browerInfo, userAgent, "Edg")) {
		return browerInfo;
	}
	if (getInfo(browerInfo, userAgent, "Chrome")) {
		return browerInfo;
	}
	if (getInfo(browerInfo, userAgent, "Safari")) {
		return browerInfo;
	}
	return browerInfo;
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
		var support = "";
		for (var key in minVer) {
			support += key + " " + minVer[key] + "+、";
		}
		$("versionCheck-support").innerHTML = support.slice(0, -2);
	}
}
checkBrower();
