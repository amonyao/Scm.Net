<template>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" @click="open_dialog()"><sc-icon icon="sc-plus" /></el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(1)"><sc-icon
								icon="sc-check-circle-line" /></el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(2)"><sc-icon
								icon="sc-pause-circle-line" /></el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0" @click="delete_list"><sc-icon
								icon="sc-close-circle-line" /></el-button>
					</el-tooltip>
				</el-button-group>
				<el-divider direction="vertical"></el-divider>
				<el-button type="primary" @click="resetData()">
					<sc-icon icon="sc-plus" />
				</el-button>
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="param.key" clearable placeholder="关键字" />
					<el-button type="primary" @click="search"><sc-icon icon="sc-search" /></el-button>
				</div>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :tableName="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<el-table-column align="center" fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
				<el-table-column label="操作" align="center" fixed="right" width="140">
					<template #default="scope">
						<el-button text type="primary" size="small" @click="open_dialog(scope.row)">
							编辑
						</el-button>
						<el-divider direction="vertical" />
						<el-popconfirm title="确定删除吗？" @confirm="delete_item(scope.row)">
							<template #reference>
								<el-button text type="primary" size="small">删除</el-button>
							</template>
						</el-popconfirm>
					</template>
				</el-table-column>
				<template #row_status="scope">
					<el-tooltip :content="scope.row.row_status ? '正常' : '停用'" placement="right">
						<el-switch v-model="scope.row.row_status" :active-value="1" :inactive-value="2"
							@change="status_item($event, scope.row)">
						</el-switch>
					</el-tooltip>
				</template>
			</scTable>
		</el-main>
		<edit ref="edit" @complete="complete" />
	</el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
	},
	data() {
		return {
			tableName: 'scm_mgr_unit',
			apiObj: this.$API.mgrunit.page,
			list: [],
			param: {
				key: ""
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'codes', label: '系统代码', width: 100, align: 'left' },
				{ prop: 'codec', label: '机构编码', width: 100, align: 'left' },
				{ prop: 'names', label: '机构简称', width: 100, align: 'left' },
				{ prop: 'namec', label: '机构全称', minWidth: 100, align: 'left' },
				{ prop: 'telephone', label: '固话', width: 140, align: 'left' },
				{ prop: 'contact', label: '联系人', width: 100, align: 'left' },
				{ prop: 'cellphone', label: '联系手机', width: 140, align: 'left' },
				{ prop: 'row_status', label: '状态', width: 80 },
				{ prop: 'create_time', label: '创建时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'create_names', label: '创建人员', width: 100 },
				{ prop: 'update_time', label: '更新时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'update_names', label: '更新人员', width: 100 },
			],
		};
	},
	mounted() {
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.mgrunit.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.mgrunit.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.mgrunit.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.mgrunit.delete, this.selection);
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
		resetData() {
			if (!this.selection) {
				return;
			}

			var row = this.selection[0];
			var res = this.$API.mgrunit.resetData.get(row.id);
			if (res == null) {
				return;
			}
		},
		clearData() {

		},
	},
};
</script>
