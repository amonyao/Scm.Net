<template>
	<el-container>
		<el-aside width="260px">
			<sc-cat @change="cat_change" :appId="this.$API.scmgtdheader.SYS_ID"></sc-cat>
		</el-aside>
		<el-container>
			<el-header>
				<div class="left-panel">
					<el-button type="primary" @click="open_dialog()"><sc-icon name="sc-plus" /></el-button>
					<el-divider direction="vertical"></el-divider>
					<el-radio-group v-model="model" @change="changeView()">
						<el-radio-button label="1">表格</el-radio-button>
						<el-radio-button label="2">列表</el-radio-button>
						<el-radio-button label="3">三列</el-radio-button>
						<el-radio-button label="4">四象限</el-radio-button>
					</el-radio-group>
				</div>
				<div class="right-panel">
					<div class="right-panel-search">
						<el-input v-model="param.key" clearable placeholder="关键字" />
						<el-button type="primary" @click="search"><sc-icon name="sc-search" /></el-button>
					</div>
				</div>
			</el-header>
			<el-main :class="{ nopadding: model == 1 }">
				<div v-if="model == 1" class="grid_row">
					<scTable ref="table" :data="list" :column="column" row-key="id" @menu-handle="menuHandle"
						@selection-change="selectionChange" :hide-pagination="true" :hide-do="true">
						<el-table-column align="center" fixed type="selection" width="60" />
						<el-table-column label="#" type="index" width="50"></el-table-column>
						<el-table-column label="操作" align="center" fixed="right" width="140">
							<template #default="scope">
								<el-button text type="primary" size="small" @click="open_dialog(scope.row)">
									编辑
								</el-button>
								<el-divider direction="vertical" />
								<el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
									<template #reference>
										<el-button text type="primary" size="small">删除</el-button>
									</template>
								</el-popconfirm>
							</template>
						</el-table-column>
					</scTable>
				</div>
				<div v-else-if="model == 2" class="grid_row">
					<sc-list :data="list" :hide-do="true" class="gtd-list col3" :canSelected="true">

						<template #item="{ item }">
							<div class="gtd-item">
								<div class="thumb" @click.stop="changeHandle(item)">
									<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
									<sc-icon name="sc-check-square-line" class="check-icon" v-if="item.handle == 3" />
								</div>
								<div style="width: 100%;" @click="itemClick(item)">
									<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
									<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
								</div>
							</div>
						</template>
					</sc-list>
				</div>
				<div v-else-if="model == 3" class="grid_row">
					<sc-list :data="handleList1" :hide-do="true" class="gtd-list col3" :canSelected="true" header="待办">

						<template #item="{ item }">
							<div class="gtd-item">
								<div class="thumb" @click.stop="changeHandle(item)">
									<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
									<sc-icon name="sc-check-square-line" class="check-icon" v-if="item.handle == 3" />
								</div>
								<div style="width: 100%;" @click="itemClick(item)">
									<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
									<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
								</div>
							</div>
						</template>
					</sc-list>
					<sc-list :data="handleList2" :hide-do="true" class="gtd-list col3" :canSelected="true" header="进行中">

						<template #item="{ item }">
							<div class="gtd-item">
								<div class="thumb" @click.stop="changeHandle(item)">
									<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
									<sc-icon name="sc-check-square-line" class="check-icon" v-if="item.handle == 3" />
								</div>
								<div style="width: 100%;" @click="itemClick(item)">
									<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
									<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
								</div>
							</div>
						</template>
					</sc-list>
					<sc-list :data="handleList3" :hide-do="true" class="gtd-list col3" :canSelected="true" header="已完成">

						<template #item="{ item }">
							<div class="gtd-item">
								<div class="thumb" @click.stop="changeHandle(item)">
									<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
									<sc-icon name="sc-check-square-line" class="check-icon" v-if="item.handle == 3" />
								</div>
								<div style="width: 100%;" @click="itemClick(item)">
									<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
									<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
								</div>
							</div>
						</template>
					</sc-list>
				</div>
				<div v-else-if="model == 4" class="grid_row">
					<div class="grid_col">
						<sc-list ref="priority1" :data="priorityList1" :hide-do="true" class="gtd-list row2_top"
							:canSelected="true" header="紧急且重要">

							<template #item="{ item }">
								<div class="gtd-item">
									<div class="thumb" @click.stop="changeHandle(item)">
										<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
										<sc-icon name="sc-check-square-line" class="check-icon"
											v-if="item.handle == 3" />
									</div>
									<div style="width: 100%;" @click="itemClick(item)">
										<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
										<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
									</div>
								</div>
							</template>
						</sc-list>
						<sc-list ref="priority2" :data="priorityList2" :hide-do="true" class="gtd-list row2_bot"
							:canSelected="true" header="紧急不重要">

							<template #item="{ item }">
								<div class="gtd-item">
									<div class="thumb" @click.stop="changeHandle(item)">
										<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
										<sc-icon name="sc-check-square-line" class="check-icon"
											v-if="item.handle == 3" />
									</div>
									<div style="width: 100%;" @click="itemClick(item)">
										<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
										<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
									</div>
								</div>
							</template>
						</sc-list>
					</div>
					<div class="grid_col">
						<sc-list ref="priority3" :data="priorityList3" :hide-do="true" class="gtd-list row2_top"
							:canSelected="true" header="不紧急但重要">

							<template #item="{ item }">
								<div class="gtd-item">
									<div class="thumb" @click.stop="changeHandle(item)">
										<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
										<sc-icon name="sc-check-square-line" class="check-icon"
											v-if="item.handle == 3" />
									</div>
									<div style="width: 100%;" @click="itemClick(item)">
										<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
										<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
									</div>
								</div>
							</template>
						</sc-list>
						<sc-list ref="priority4" :data="priorityList4" :hide-do="true" class="gtd-list row2_bot"
							:canSelected="true" header="不紧急不重要">

							<template #item="{ item }">
								<div class="gtd-item">
									<div class="thumb" @click.stop="changeHandle(item)">
										<sc-icon name="sc-square-line" class="check-icon" v-if="item.handle == 1" />
										<sc-icon name="sc-check-square-line" class="check-icon"
											v-if="item.handle == 3" />
									</div>
									<div style="width: 100%;" @click="itemClick(item)">
										<div class="title" :class="{ 'gtd-done': item.checked }">{{ item.title }}</div>
										<div class="summary">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
									</div>
								</div>
							</template>
						</sc-list>
					</div>
				</div>
				<div v-else class="grid_row">
				</div>
			</el-main>
			<el-footer>
				<div class="right-panel-search">
					<el-input v-model="formData.title" clearable placeholder="请输入待办任务" @keydown="keydown" />
					<el-button type="primary" @click="create"><sc-icon name="sc-plus" /></el-button>
				</div>
			</el-footer>
			<edit ref="edit" @complete="complete" />
		</el-container>
	</el-container>
</template>

<script>
import { defineAsyncComponent } from "vue";
export default {
	name: 'scm_gtd',
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
		// handle: defineAsyncComponent(() => import("./handle")),
		// priority: defineAsyncComponent(() => import("./priority")),
	},
	data() {
		return {
			list: [],
			handleList1: [],
			handleList2: [],
			handleList3: [],
			priorityList1: [],
			priorityList2: [],
			priorityList3: [],
			priorityList4: [],
			param: {
				cat_id: '0',
				create_time: '',
				key: ''
			},
			model: 2,
			selectCat: {},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'title', label: '标题', minWidth: 160 },
				{ prop: 'priority', label: '优先级', width: 100 },
				{ prop: 'remind', label: '提醒标识', width: 100 },
				{ prop: 'notice', label: '提示方式', width: 100 },
				{ prop: 'last_time', label: '上次提醒时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'next_time', label: '下次提醒时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'update_time', label: '更新时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'update_names', label: '更新人员', width: 100 },
				{ prop: 'create_time', label: '创建时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'create_names', label: '创建人员', width: 100, },
			],
			formData: {
				cat_id: '0',
				title: '',
			}
		};
	},
	mounted() {
		this.search();
	},
	methods: {
		complete() {
			this.search();
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.scmgtdheader.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.scmgtdheader.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.scmgtdheader.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.scmgtdheader.delete, this.selection);
		},
		open_dialog(row) {
			this.$refs.edit.open(row);
		},
		selectionChange(selection) {
			this.selection = selection;
		},
		menuHandle(obj) {
			if (obj.command == "add") {
				this.open_dialog();
				return;
			}
			if (obj.command == "edit") {
				this.open_dialog(obj.row);
				return;
			}
			if (obj.command == "delete") {
				this.delete_item(obj.row);
				return;
			}
		},
		cat_change(obj) {
			this.selectCat = obj;
			if (obj) {
				this.param.cat_id = obj.id;
				this.formData.cat_id = obj.id;
			}
			this.search();
		},
		keydown(e) {
			if (e.keyCode != 13) {
				return;
			}
			this.create();
		},
		async create() {
			let res = await this.$API.scmgtdheader.add.post(this.formData);
			if (res.code == 200) {
				this.$emit("complete");
				this.visible = false;
				this.$message.success("保存成功");
				this.formData.title = '';
				this.search();
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
		changeView() {
		},
		itemClick(item) {
			this.$refs.edit.open(item);
		},
		async search() {
			var res = await this.$API.scmgtdheader.list.get(this.param);
			if (!res || res.code != 200) {
				return;
			}

			this.list = res.data;
			this.handleList1.length = 0;
			this.handleList2.length = 0;
			this.handleList3.length = 0;
			this.priorityList1.length = 0;
			this.priorityList2.length = 0;
			this.priorityList3.length = 0;
			this.priorityList4.length = 0;
			for (let index = 0; index < this.list.length; index++) {
				const element = this.list[index];
				this.checkHandle(element);
				this.checkLevel(element);
			}
		},
		checkHandle(element) {
			if (element.handle == 1) {
				this.handleList1.push(element);
				return;
			}
			if (element.handle == 2) {
				this.handleList2.push(element);
				return;
			}
			if (element.handle == 3) {
				element.checked = true;
				this.handleList3.push(element);
				return;
			}
		},
		checkLevel(element) {
			if (element.priority == 1) {
				this.priorityList1.push(element);
				return;
			}
			if (element.priority == 2) {
				this.priorityList2.push(element);
				return;
			}
			if (element.priority == 3) {
				this.priorityList3.push(element);
				return;
			}
			if (element.priority == 4) {
				this.priorityList4.push(element);
				return;
			}
		},
		async changeHandle(item) {
			if (!item) {
				return false;
			}

			var handle = item.handle;
			if (handle == 1) {
				handle = 3;
			} else if (handle == 3) {
				handle = 1;
			}
			else {
				return false;
			}

			var res = await this.$API.scmgtdheader.handle.post({ 'id': item.id, 'handle': handle });
			if (!res || res.code != 200) {
				return false;
			}

			this.search();
			return false;
		}
	},
};
</script>

<style scoped>
.grid_row {
	display: flex;
	justify-content: space-between;
	flex-direction: row;
	align-items: stretch;
	height: 100%;
	overflow: hidden;
	margin: 0 -5px;
}

.grid_col {
	display: flex;
	justify-content: space-between;
	align-items: center;
	flex-direction: column;
	flex-basis: 100%;
	margin: 0 5px;
}

.gtd-list {
	background-color: var(--bg-color);
	height: 100%;
	overflow-y: scroll;
}

.col3 {
	margin: 0 5px;
}

.row2_top {
	margin-bottom: 5px;
}

.row2_bot {
	margin-top: 5px;
}

.gtd-item {
	display: flex;
	align-items: center;
	width: 100%;
}

.gtd-item .thumb {
	width: 20px;
	height: 32px;
	margin-left: 10px;
	margin-right: 10px;
	line-height: 32px;
}

.gtd-item .title {
	color: var(--el-text-color-primary);
	font-size: var(--el-font-size-base);
	font-weight: bold;
	line-height: 1.3;
}

.gtd-item .summary {
	color: var(--el-text-color-regular);
	font-size: var(--el-font-size-base);
	line-height: 1.3;
	margin-top: 5px;
}

.gtd-item .gtd-done {
	text-decoration: line-through;
	color: var(--el-text-color-regular);
}

.check-icon {
	cursor: pointer;
	font-size: 24px;
}
</style>
