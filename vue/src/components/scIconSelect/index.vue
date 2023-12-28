<!--
 * @Descripttion: 图标选择器组件
 * @version: 2.0
 * @Author: sakuya
 * @Date: 2021年7月27日10:02:46
 * @LastEditors: sakuya
 * @LastEditTime: 2022年6月6日13:48:49
-->

<template>
	<div class="sc-icon-select">
		<div class="sc-icon-select__wrapper" :class="{ hasValue: value }" @click="open">
			<el-button style="padding: 7px;">
				<sc-icon :icon="value || 'sc-reduce-btn'" style="margin: 0px;" :size="16"></sc-icon>
			</el-button>
		</div>
		<el-dialog title="图标选择器" v-model="dialogVisible" :width="760" destroy-on-close append-to-body>
			<div class="sc-icon-select__dialog" style="margin: -20px 0 -10px 0">
				<el-form :rules="{}">
					<el-form-item prop="searchText">
						<el-input class="sc-icon-select__search-input" v-model="searchText" placeholder="搜索" clearable>
							<template #prepend>
								<sc-icon icon="sc-search" />
							</template>
							<template #append>
								<el-select v-model="mode" placeholder="Select" style="width: 115px">
									<el-option label="线型" :value="0" />
									<el-option label="填充" :value="1" />
								</el-select>
							</template>
						</el-input>
					</el-form-item>
				</el-form>
				<el-tabs>
					<el-tab-pane v-for="item in data" :key="item.name" lazy>
						<template #label>
							{{ item.name }}
							<el-tag size="small" type="info">
								{{ item.size }}
							</el-tag>
						</template>
						<div class="sc-icon-select__list">
							<el-scrollbar>
								<ul>
									<el-empty v-if="item.icons.length == 0" :image-size="100" description="未查询到相关图标" />
									<li v-for="icon in item.icons" :key="icon" @click="selectIcon(icon)" :title="icon.desc">
										<div class="icon-item">
											<div class="icon-info">
												<span :class="getIcon(icon)"></span>
											</div>
											<div class="icon-desc">
												{{ icon.desc }}
											</div>
										</div>
									</li>
								</ul>
							</el-scrollbar>
						</div>
					</el-tab-pane>
				</el-tabs>
			</div>
			<template #footer>
				<el-button @click="clear" text>清除</el-button>
				<el-button @click="dialogVisible = false">取消</el-button>
			</template>
		</el-dialog>
	</div>
</template>

<script>
import config from "@/config/iconSelect";

export default {
	props: {
		modelValue: { type: String, default: "" },
		disabled: { type: Boolean, default: false },
	},
	data() {
		return {
			value: "",
			dialogVisible: false,
			mode: 0,
			data: [],
			searchText: "",
		};
	},
	watch: {
		modelValue(val) {
			this.value = val;
		},
		value(val) {
			this.$emit("update:modelValue", val);
		},
		searchText(val) {
			this.search(val);
		},
	},
	mounted() {
		this.value = this.modelValue;
		this.data.push(...config.icons);
	},
	methods: {
		open() {
			if (this.disabled) {
				return false;
			}
			this.dialogVisible = true;
		},
		selectIcon(icon) {
			if (!icon) {
				return false;
			}
			this.value = this.getName(icon);
			this.dialogVisible = false;
		},
		clear() {
			this.value = "";
			this.dialogVisible = false;
		},
		search(text) {
			var filterData = [];
			if (text) {
				config.icons.forEach((t) => {
					var icons = t.icons.filter((n) => n.desc.includes(text));
					var cat = { name: t.name, icons: icons, size: icons.length };
					filterData.push(cat);
				});
			} else {
				filterData = config.icons;
			}
			this.data = filterData;
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
		}
	},
};
</script>

<style scoped>
.sc-icon-select {
	display: inline-flex;
}

.sc-icon-select__wrapper {
	cursor: pointer;
	display: inline-flex;
}

.sc-icon-select__wrapper:deep(.el-input__wrapper).is-focus {
	box-shadow: 0 0 0 1px var(--el-input-hover-border-color) inset;
}

.sc-icon-select__wrapper:deep(.el-input__inner) {
	flex-grow: 0;
	width: 0;
}

.sc-icon-select__wrapper:deep(.el-input__icon) {
	margin: 0;
	font-size: 16px;
}

.sc-icon-select__wrapper.hasValue:deep(.el-input__icon) {
	color: var(--el-text-color-regular);
}

.sc-icon-select__list {
	height: 270px;
	overflow: auto;
}

.sc-icon-select__list ul {}

.sc-icon-select__list li {
	display: inline-block;
	width: 80px;
	height: 80px;
	margin: 5px;
	vertical-align: top;
	transition: all 0.1s;
	border-radius: 4px;
	position: relative;
}

.sc-icon-select__list li i {
	display: inline-block;
	width: 100%;
	height: 100%;
	font-size: 26px;
	color: #6d7882;
	display: flex;
	justify-content: center;
	align-items: center;
	border-radius: 4px;
}

.sc-icon-select__list li:hover {
	box-shadow: 0 0 1px 4px var(--el-color-primary);
	background: var(--el-color-primary-light-9);
}

.sc-icon-select__list li:hover i {
	color: var(--el-color-primary);
}

.icon-item {
	display: flex;
	flex-direction: column;
	justify-content: space-between;
	align-items: center;
	cursor: pointer;
	width: 100%;
	height: 100%;
}

.icon-item .icon-info {
	text-align: center;
	flex-basis: 100%;
	display: flex;
	align-items: center;
	-webkit-transition: font-size 0.25s linear, width 0.25s linear;
	-moz-transition: font-size 0.25s linear, width 0.25s linear;
	transition: font-size 0.25s linear, width 0.25s linear;
	font-size: 32px;
}

.icon-item .icon-desc {
	color: #666;
	text-align: center;
	width: 100%;
	padding: 2px 8px;
	display: block;
	word-break: keep-all;
	white-space: nowrap;
	overflow: hidden;
}
</style>
