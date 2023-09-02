<template>
	<el-container>
		<el-header>
			111
		</el-header>
		<el-main class="nopadding">
			<div class="page">
				<scWaterfall :data="list" @preload="preload" @showContent="showContent" :columnCount="3">
					<template #item="{ item }">
						<img :data-src="item.src" :alt="item.names" :style="{ height: item._dim + 'px' }" />
						<div>
							<label>这是说明文字</label>
						</div>
					</template>
				</scWaterfall>
			</div>
		</el-main>
	</el-container>
</template>
<script>
export default {
	data() {
		return {
			apiObj: "",
			list: [],
			imgWidth: 100,
			param: {
				types_id: "0",
				row_status: "1",
				create_time: "",
				key: "",
			},
		};
	},
	mounted() {
		//this.$SCM.list_dic(this.types_list, 'article_type', true);
		this.list = this.testData();
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		testData() {
			var imgArr = [
				{
					src: "https://vitejs.cn/vite3-cn/logo-with-shadow.png",
					width: 595,
					height: 808,
				},
				{
					src: "https://fscdn.zto.com/fs8/M02/94/F3/wKhBD19OTuuAedCpAAIKGsFFPqc560.png",
					width: 595,
					height: 808,
				},
				{
					src: "https://vitejs.cn/vite3-cn/logo-with-shadow.png",
					width: 188,
					height: 121,
				},
				{
					src: "https://vitejs.cn/vite3-cn/logo-with-shadow.png",
					width: 1317,
					height: 845,
				},
				{
					src: "https://fscdn.zto.com/fs8/M02/94/F3/wKhBD19OTuuAedCpAAIKGsFFPqc560.png",
					width: 1349,
					height: 799,
				},
			];

			var list = [];
			for (let i = 0; i < 40; i++) {
				var idx = Math.round(Math.random() * (imgArr.length - 1));
				var img = imgArr[idx];
				//var tmp = this.imgWidth * img.height / img.width;
				img.height = Math.random() * img.height + 100;
				list.push(img);
			}

			return list;
		},
		preload(item) {
			// 指定大小
			if (item.height && item.width) {
				this.calSize(item, item.width, item.height, this.imgWidth);
				return;
			}

			// 无图则把高度设置为0
			var src = item.src;
			if (!src) {
				this.calSize(item, 0, 0, this.imgWidth);
				return;
			}

			let img = new Image();
			img.src = src;
			img.onload = img.onerror = (e) => {
				var width = 0;
				var height = 0;
				if (e.type === "error") {
					item._error = true;
				} else if (e.type === "load") {
					width = img.width;
					height = img.height;
				}

				this.calSize(item, width, height, this.imgWidth);
			};
		},
		// 明细展示
		showContent(item) {
			let img = item.children[0];
			if (img) {
				if (img.loaded) {
					return;
				}

				img.src = img.getAttribute("data-src");
				img.style.opacity = 1;
				img.style.transform = "scale(1)";
				img.loaded = true;
			}
		},
		// 计算图片大小
		calSize(item, width, height, def) {
			var tmp = def;
			if (width && height) {
				tmp = Math.round((this.imgWidth * height) / width);
			}
			item._dim = tmp;
			// ++this.loadedCount;
			// // 当前图片都与处理完，则加载图片
			// if (this.apiData.length === this.loadedCount) {
			//     this.preloaded();
			// }
		},
	},
};
</script>
<style scoped>
.page {
	width: 100%;
	height: 100%;
	margin: 0 auto;
	padding: 15px;
}

.sc-waterfall-item img {
	width: 100%;
	border-radius: 10px;
	opacity: 0;
	transform: scale(0.5);
	transition: all 0.6s;
	transition-delay: 0.1s;
}
</style>
