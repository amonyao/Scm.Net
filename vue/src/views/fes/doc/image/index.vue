<template>
	<el-container>
		<el-aside width="260px" v-loading="showGrouploading">
			<el-container>
				<el-header>
					<sc-select v-model="param.type" @change="typeChange" :data="type_list">
					</sc-select>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="group" class="menu" node-key="id" default-expand-all :data="group" :props="props"
						:current-node-key="''" :highlight-current="true" :expand-on-click-node="false"
						@node-click="groupClick">
						<template #default="{ node, data }">
							<span class="scmui-item-node">
								<span class="label">{{ node.label }}</span>
								<span class="opt" v-if="data.uri">
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
					<el-input v-model="param.path" :style="{ width: '480px' }">
						<template #prepend>
							<el-icon><el-icon-folder-opened /></el-icon>
						</template>
						<template #append>
							<el-button icon="el-icon-refresh" @click="refresh" />
						</template>
					</el-input>
				</div>
				<div class="right-panel">
					<el-button icon="el-icon-copy-document" plain type="success" :disabled="!selectedFile"
						v-copy="selectedUrl">复制地址</el-button>
					<el-button icon="el-icon-view" plain type="success" :disabled="!selectedFile" @click="file_view" />
					<el-button icon="el-icon-delete" plain type="danger" :disabled="!selectedFile" @click="file_del" />
					<el-button icon="el-icon-download" plain type="primary" :disabled="!selectedFile" @click="file_down" />
					<el-button icon="el-icon-upload" plain type="primary" @click="open_dialog">本地上传</el-button>
				</div>
			</el-header>
			<el-main class="nopadding" style="padding: 10px">
				<el-scrollbar ref="scrollbar">
					<el-empty v-if="files.length == 0" description="无数据" :image-size="80"></el-empty>
					<sc-waterfall :data="files" @preload="preload" @showContent="showContent">
						<template #item="{ item }">
							<img class="img" :data-src="serverApi + item.id" :alt="item.namec"
								:style="{ 'height': item._dim + 'px', }" />
							<div>
								<label>{{ item.namec }}</label>
							</div>
						</template>
					</sc-waterfall>
				</el-scrollbar>
			</el-main>
			<el-image-viewer @close="closeImgViewer" :url-list="previewList" v-if="showImageViewer" />
			<sc-text-viewer ref="textViewer" />
			<sc-upload ref="upload" @complete="complete" :api-obj="this.$API.fesdocfile.upload" :multiple="true" />
		</el-container>
	</el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";

export default {
	components: {
		scUpload: defineAsyncComponent(() => import("@/components/scUpload")),
		scTextViewer: defineAsyncComponent(() => import("@/components/scTextViewer")),
	},
	data() {
		return {
			serverApi: undefined,//服务器地址
			showGrouploading: false,//目录树加载标识
			group: [],//目录列表
			props: { label: "name" },
			param: {
				type: 0,
				path: '/',
				key: '',
			},
			type_list: [],//文件类型列表
			files: [],//文件列表
			previewList: [],//预览列表
			value: [],
			selectedFile: undefined,//当前选中的文件
			selectedUrl: undefined,//文件绝对路径
			showImageViewer: false,
			showTextViewer: false,
			imgWidth: 100,
		};
	},
	mounted() {
		this.serverApi = this.$API.fesdocimage.model.url;
		this.init();
		this.initFiles();
		this.$SCM.list_dic(this.type_list, 'file_type', true);
	},
	methods: {
		async init() {
			const res = await this.$API.fesrescat.list.get({ path: '/' });
			if (!res || res.code != 200) {
				return;
			}

			this.group = res.data;
		},
		async initFiles() {
			this.previewList = [];
			this.value = [];
			const res = await this.$API.fesdocimage.page.get(this.param);
			if (!res || res.code != 200) {
				return;
			}

			this.files = res.data.items;
		},
		refresh() {
			this.value = [];
			this.initFiles();
		},
		complete() {
			this.init();
			this.initFiles();
		},
		open_dialog() {
			this.$refs.upload.open(this.param.path);
		},
		file_view() {
			if (!this.selectedFile) {
				return;
			}
			if (this.selectedFile.type == 2) {
				this.viewText();
				return;
			}
			if (this.selectedFile.type == 3) {
				this.viewImage();
				return;
			}
		},
		viewImage() {
			this.showImageViewer = true;
		},
		closeImgViewer() {
			this.showImageViewer = false;
		},
		viewText() {
			this.$refs.textViewer.showUri(this.selectedUrl);
		},
		select(item, row) {
			this.previewList = [];
			this.selectedUrl = this.serverApi + row.uri;
			this.previewList = [this.selectedUrl];
			if (this.value.includes(item)) {
				this.value = "";
				this.selectedFile = undefined;
			} else {
				this.value = item;
				this.selectedFile = row;
			}
		},
		typeChange() {
			this.initFiles();
		},
		groupClick(data) {
			this.param.path = data.uri;
			this.selectedFile = undefined;
			this.initFiles();
		},
		remove_tree(node, data) {
			this.$confirm(
				`确定删除 【${data.name}】 文件夹吗，如果删除将会删除当前文件夹及子目录和文件？`,
				"提示",
				{
					type: "warning",
					confirmButtonText: "确定",
					cancelButtonText: "取消",
				}
			)
				.then(async () => {
					const loading = this.$loading();
					var res = await this.$API.fesdocimage.delFolder.delete(data.uri);
					loading.close();
					if (res.code == 200) {
						this.param.path = "/upload/";
						this.init();
						this.initFiles();
						this.$message.success("删除成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				})
				.catch(() => { });
		},
		file_del() {
			this.$confirm(
				`确定删除选中的 【${this.selectedFile.name}】 吗？`,
				"提示",
				{
					type: "warning",
					confirmButtonText: "确定",
					cancelButtonText: "取消",
				}
			)
				.then(async () => {
					const loading = this.$loading();
					var res = await this.$API.fesdocimage.delFile.delete(this.selectedFile.uri);
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
			const fileData = this.serverApi + this.selectedFile.uri;
			const url = window.URL.createObjectURL(
				new Blob([fileData], {
					type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8",
				})
			);
			const link = document.createElement("a");
			link.href = url;
			link.setAttribute("download", this.selectedFile.name);
			document.body.appendChild(link);
			link.click();
			document.body.removeChild(link);
			window.URL.revokeObjectURL(url);
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
				}
				else if (e.type === 'load') {
					width = img.width;
					height = img.height;
				}

				this.calSize(item, width, height, this.imgWidth);
			}
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
				tmp = Math.round(this.imgWidth * height / width);
			}
			item._dim = tmp;
			// ++this.loadedCount;
			// // 当前图片都与处理完，则加载图片
			// if (this.apiData.length === this.loadedCount) {
			//     this.preloaded();
			// }
		}
	},
};
</script>
<style scoped>
.sc-waterfall-item img {
	width: 100%;
	border-radius: 10px;
	opacity: 0;
	transform: scale(0.5);
	transition: all 0.6s;
	transition-delay: 0.1s;
}
</style>
