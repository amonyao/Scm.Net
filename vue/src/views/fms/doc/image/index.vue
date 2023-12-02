<template>
	<el-container>
		<el-aside width="260px" v-loading="showGrouploading">
			<el-container>
				<el-header>
					<el-select v-model="param.types" placeholder="请选择" @change="typeChange">
						<el-option v-for="item in options" :key="item.value" :label="item.label"
							:value="item.value"></el-option>
					</el-select>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="group" class="menu" node-key="id" default-expand-all :data="group" :props="props"
						:current-node-key="''" :highlight-current="true" :expand-on-click-node="false"
						@node-click="groupClick">
						<template #default="{ node, data }">
							<span class="custom-tree-node">
								<span class="label">{{ node.label }}</span>
								<span class="do" v-if="data.routes">
									<el-icon @click.stop="remove_tree(node, data)"><el-icon-delete /></el-icon>
								</span>
							</span>
						</template>
					</el-tree>
				</el-main>
			</el-container>
		</el-aside>
		<el-container>
			<el-header>
				<div class="left-panel">
					<el-input v-model="param.path" :style="{ width: '360px' }">
						<template #prepend>
							<el-icon><el-icon-folder-opened /></el-icon>
						</template>
						<template #append>
							<el-button icon="el-icon-refresh" @click="refresh" />
						</template>
					</el-input>
				</div>
				<div class="right-panel">
					<el-button icon="el-icon-copy-document" plain type="success" :disabled="!selectFile"
						v-copy="copyUrl">复制地址</el-button>
					<el-button icon="el-icon-picture" plain type="success" :disabled="!selectFile"
						@click="lookImg">查看原图</el-button>
					<el-button icon="el-icon-delete" plain type="danger" :disabled="!selectFile" @click="file_del" />
					<el-button icon="el-icon-download" plain type="primary" :disabled="!selectFile" @click="file_down" />
					<el-button icon="el-icon-upload" plain type="primary" @click="open_dialog">本地上传</el-button>
				</div>
			</el-header>
			<el-main class="nopadding" style="padding: 10px">
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
	</el-container>
</template>
<script>
export default {
	data() {
		return {
			apiObj: "",
			showGrouploading: false,
			groupFilterText: "",
			group: [],
			options: [
				{
					value: "0",
					label: "所有文件",
				},
				{
					value: "file",
					label: "文件",
				},
				{
					value: "image",
					label: "图片",
				},
			],
			props: {
				label: "path",
			},
			list: [],
			imgWidth: 100,
			param: {
				types: '0',
				row_status: 1,
				create_time: '',
				key: '',
			},
			selectFile: undefined,
			copyUrl: undefined,
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
			img.onerror = () => {
				item._error = true;
			};
			img.onload = () => {
				this.calSize(item, img.width, img.height, this.imgWidth);
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
		},
        typeChange() {
        },
        groupClick() {

        },
		lookImg() {
			this.showImageViewer = true;
		},
		file_del() {
			this.$confirm(
				`确定删除选中的 【${this.selectFile.name}】 吗？`,
				"提示",
				{
					type: "warning",
					confirmButtonText: "确定",
					cancelButtonText: "取消",
				}
			)
				.then(async () => {
					const loading = this.$loading();
					var res = await this.$API.sysfile.delFile.delete(
						this.selectFile.fileName
					);
					if (res.code == 200) {
						this.initFiles();
						loading.close();
						this.$message.success("删除成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				})
				.catch(() => { });
		},
		file_down() {
			const fileData = this.serverApi + this.selectFile.fileName;
			const url = window.URL.createObjectURL(
				new Blob([fileData], {
					type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8",
				})
			);
			const link = document.createElement("a");
			link.href = url;
			link.setAttribute("download", this.selectFile.name);
			document.body.appendChild(link);
			link.click();
			document.body.removeChild(link);
			window.URL.revokeObjectURL(url);
		},
		open_dialog() {
			// this.$refs.upload.open(this.param.path);
		},
		refresh() {
			// this.value = [];
			// this.initFiles();
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
