<template>
	<el-container>
		<el-header style="height: auto;">
			<sc-search @search="search" :syncSearch="true">
				<template #filter>
					<label>图标格式：</label>
					<el-switch v-model="mode" active-text="填充" inactive-text="线型" />
					<label>图标大小：</label>
					<el-slider v-model="size" :min="16" :max="128" :step="4" style="width: 120px;"></el-slider>
					<label>图标颜色：</label>
					<el-color-picker v-model="color" :predefine="predefineColors" />
				</template>
			</sc-search>
		</el-header>
		<el-main class="nopadding">
			<el-container>
				<el-aside style="width: 240px;">
					<sc-list :data="filterData" @change="changeSet">
						<template #item="{ item }">
							{{ item.name }}
							<el-tag size="small" type="info">
								{{ item.size }}
							</el-tag>
						</template>
					</sc-list>
				</el-aside>
				<el-main>
					<el-scrollbar>
						<div class="icon-list">
							<el-empty v-if="!hasIcons()" :image-size="100" description="未查询到相关图标" />
							<el-row v-else>
								<el-col :xs="6" :sm="6" :md="4" :lg="3" :xl="3" v-for="(icon, index) in iconSet.icons"
									:key="index">
									<div class="icon-item" :title="icon.desc" @click="copyCode(icon)">
										<div class="icon-info" :style="{ 'fontSize': size + 'px', 'color': color }">
											<span :class="getIcon(icon)"></span>
										</div>
										<div class="icon-desc">
											{{ icon.desc }}
										</div>
									</div>
								</el-col>
							</el-row>
						</div>
					</el-scrollbar>
				</el-main>
			</el-container>
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
			mode: false,
			size: 24,
			color: '#1a2947',
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
			iconSet: {},
			index: 0,
		};
	},
	watch: {
		searchText(val) {
			this.search(val);
		},
	},
	mounted() {
		this.data.push(...config.icons);
		this.filterData = this.data;
		this.iconSet = this.filterData[this.index];
	},
	methods: {
		changeSet(set, index) {
			this.iconSet = set;
			this.index = index;
		},
		hasIcons() {
			return this.iconSet && this.iconSet.icons && this.iconSet.icons.length > 0;
		},
		getName(icon) {
			var name = icon.name;
			if (icon.type == 'both') {
				name += (this.mode ? '-fill' : '-line');
			} else if (icon.type) {
				name += '-' + icon.type;
			}
			return 'sc-' + name;
		},
		getIcon(icon) {
			return 'scfont ' + this.getName(icon);
		},
		copyCode(icon) {
			//var code = '<span class="' + this.getIcon(icon) + '" style="color:' + this.color + ';font-size: ' + this.size + 'px;"/>';
			var code = '<sc-icon icon="' + this.getName(icon) + '" style="color:' + this.color + ';font-size: ' + this.size + 'px;"/>';
			clipboardy.write(code);
			this.$message.success('复制成功！');
		},
		search(key) {
			var filterData = [];
			if (key) {
				this.data.forEach((t) => {
					var icons = t.icons.filter((n) => n.desc.includes(key));
					var cat = { name: t.name, icons: icons, size: icons.length };
					filterData.push(cat);
				});
			} else {
				filterData = config.icons;
			}
			this.filterData = filterData;
			this.iconSet = this.filterData[this.index];
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
	color: #afb7c7;
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