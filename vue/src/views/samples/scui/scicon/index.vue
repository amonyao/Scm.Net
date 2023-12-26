<template>
	<el-container>
		<el-header style="height: auto;">
			<sc-search @search="search">
				<template #filter>
					<label>图标格式：</label>
					<el-switch v-model="mode" active-text="填充" inactive-text="线型" />
					<label>图标大小：</label>
					<el-slider v-model="size" :min="16" :max="128" :step="4" style="width: 120px;"></el-slider>
					<label>图标颜色：</label>
					<el-color-picker v-model="color" show-alpha :predefine="predefineColors" />
					<div :style="{ 'fontSize': size + 'px', 'color': color }">
						<span class="scfont sc-tixing"></span>
					</div>
				</template>
			</sc-search>
		</el-header>
		<el-main class="nopadding">
			<el-tabs>
				<el-tab-pane v-for="item in filterData" :key="item.name" lazy>
					<template #label>
						{{ item.name }}
						<el-tag size="small" type="info">
							{{ item.size }}
						</el-tag>
					</template>
					<div class="icon-list">
						<el-scrollbar>
							<ul @click="selectIcon">
								<el-empty v-if="item.icons.length == 0" :image-size="100" description="未查询到相关图标" />
								<el-row>
									<el-col :xs="6" :sm="6" :md="4" :lg="3" :xl="3" v-for="icon in item.icons" :key="icon">
										<div class="icon-item" :class="{ active: value == icon.name }" :title="icon.desc"
											@click="copyCode($event, icon)">
											<div class="icon-info" :style="{ 'fontSize': size + 'px', 'color': color }">
												<span :class="getIcon(icon)"></span>
											</div>
											<div class="icon-desc">
												{{ icon.desc }}
											</div>
										</div>
									</el-col>
								</el-row>
							</ul>
						</el-scrollbar>
					</div>
				</el-tab-pane>
			</el-tabs>
		</el-main>
	</el-container>
</template>

<script>
const clipboardy = require('clipboardy');
import config from "@/config/iconSelect";

export default {
	data() {
		return {
			param: {
				key: ''
			},
			isExpand: false,
			mode: false,
			size: 24,
			color: '#000000',
			predefineColors: [
				'#ffffff',
				'#cccccc',
				'#999999',
				'#666666',
				'#333333',
				'#000000',
				'#ff0000',
				'#00ff00',
				'#0000ff',
				'#ff4500',
				'#ff8c00',
				'#ffd700',
				'#90ee90',
				'#00ced1',
				'#1e90ff',
				'#c71585',
			],
			data: [],
			filterData: [],
			value: null,
			icons: {},
		};
	},
	mounted() {
		this.data.push(...config.icons);
		this.filterData = this.data;
	},
	methods: {
		expand() {
			this.isExpand = !this.isExpand;
		},
		getIcon(icon) {
			return 'scfont sc-' + icon.name + (this.mode ? '-fill' : '-line');
		},
		copyCode(e, icon) {
			this.value = icon.name;
			var code = '<span class="' + this.getIcon(icon) + '" style="color:' + this.color + ';font-size: ' + this.size + 'px;"/>';
			clipboardy.write(code);
			this.$message.success('复制成功！');
		},
		search(key) {
			this.param.key = key;
			if (!this.param.key) {
				this.filterData = this.data;
				return;
			}

			this.filterData = [];
			this.data.forEach(group => {
				var list = [];
				var icons = group.icons;
				icons.forEach(arr => {
					if (arr.desc && arr.desc.indexOf(this.param.key) >= 0) {
						list.push(arr)
					}
				});
				this.filterData.push({ name: group.name, icons: list, size: list.length });
			});
		}
	},
};
</script>

<style scoped>
.icon-list {}

.icon-list .icon-item {
	display: inline-block;
	display: flex;
	flex-direction: column;
	justify-content: space-between;
	align-items: center;
	border: 1px solid #fff;
	border-radius: 5px;
	width: 100%;
	height: 100%;
	min-height: 80px;
	transition: all 0.1s;
	border-radius: 4px;
	cursor: pointer;
	padding: 5px;
}

.icon-list .icon-item:hover {
	/* box-shadow: 0 0 1px 4px var(--el-color-primary); */
	background: var(--el-color-primary-light-9);
	border: 1px solid var(--el-color-primary);
}

.icon-list .icon-item .icon-info {
	text-align: center;
	flex-basis: 100%;
	display: flex;
	align-items: center;
	-webkit-transition: font-size 0.25s linear, width 0.25s linear;
	-moz-transition: font-size 0.25s linear, width 0.25s linear;
	transition: font-size 0.25s linear, width 0.25s linear;
}

.icon-list .icon-item .icon-info:hover {
	font-size: 100px;
}

.icon-list .icon-item .icon-desc {
	color: #666;
	text-align: center;
	width: 100%;
	padding: 2px 8px;
	display: block;
	word-break: keep-all;
	white-space: nowrap;
	overflow: hidden;
}

.icon-list .active {
	background: var(--el-color-primary-light-9);
	border: 1px solid var(--el-color-primary);
}
</style>