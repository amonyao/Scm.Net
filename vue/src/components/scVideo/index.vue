<!--
 * @Descripttion: xgplayer二次封装
 * @version: 1.0
 * @Author: sakuya
 * @Date: 2021年11月29日12:10:06
 * @LastEditors:
 * @LastEditTime:
-->

<template>
	<div class="sc-video" ref="scVideo"></div>
</template>

<script>
import Player from "xgplayer";
import HlsPlayer from "xgplayer-hls";
import 'xgplayer/dist/index.min.css';

export default {
	props: {
		//视频路径
		src: { type: String, required: true, default: "" },
		//封面
		poster: { type: String, default: "" },
		//音量
		volume: { type: Number, default: 0.8 },
		//是否显示控制
		controls: { type: Boolean, default: true },
		//是否直播场景
		isLive: { type: Boolean, default: false },
		//自动播放
		autoplay: { type: Boolean, default: false },
		//循环播放
		loop: { type: Boolean, default: false },
		//初始化显示首帧
		videoInit: { type: Boolean, default: false },
		//画中画
		pip: { type: Boolean, default: false },
		//倍速播放
		playbackRate: { type: [Array, String], default: () => [0.5, 0.75, 1, 1.5, 2] },
		//记忆播放
		lastPlayTime: { type: Number, default: 0 },
		//弹幕
		danmu: { type: [Array, String], default: "" },
		//源切换
		resource: { type: Array, default: () => [] },
		//进度条特殊点标记
		progressDot: { type: Array, default: () => [] },
		//下载
		download: { type: Boolean, default: false },
	},
	data() {
		return {
			player: null,
		};
	},
	mounted() {
		if (this.isLive) {
			this.initHls();
		} else {
			this.init();
		}
	},
	methods: {
		init() {
			this.player = new Player({
				el: this.$refs.scVideo,
				url: this.src,
				height: '100%',
				width: '100%',
				poster: this.poster,
				lang: "zh-cn",
				volume: this.volume,
				autoplay: this.autoplay,
				loop: this.loop,
				videoInit: this.videoInit,
				playbackRate: this.playbackRate,
				lastPlayTime: this.lastPlayTime,
				pip: this.pip,
				controls: this.controls,
				danmu: this.formatDanmu(this.danmu),
				progressDot: this.progressDot,
				download: this.download,
			});
			this.player.emit("resourceReady", this.resource);
		},
		initHls() {
			this.player = new HlsPlayer({
				el: this.$refs.scVideo,
				url: this.src,
				fluid: true,
				poster: this.poster,
				isLive: true,
				ignores: ["time", "progress"],
				lang: "zh-cn",
				volume: this.volume,
				pip: this.pip,
				controls: this.controls,
			});
		},
		formatDanmu(danmu) {
			if (!danmu) {
				return false;
			}
			let newDanmu = [];
			danmu.forEach((item) => {
				newDanmu.push({
					id: item.id || "",
					start: item.start || 0,
					txt: item.txt || "",
					duration: 10000,
					mode: item.mode || "scroll",
					style: item.style || {},
				});
			});
			return {
				comments: newDanmu,
			};
		},
	},
};
</script>