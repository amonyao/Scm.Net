<template>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" @click="open_dialog()"><sc-icon name="sc-plus" /></el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(1)">
							<sc-icon name="sc-check-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(2)">
							<sc-icon name="sc-pause-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0" @click="delete_list">
							<sc-icon name="sc-close-circle-line" />
						</el-button>
					</el-tooltip>
				</el-button-group>
			</div>
			<div class="right-panel">
				<el-input v-model="param.key" clearable placeholder="关键字">
					<template #append>
						<el-button type="primary" @click="search()"><sc-icon name="sc-search" /></el-button>
					</template>
				</el-input>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :table-name="tableName" :api-obj="apiObj" :column="column" hide-pagination is-tree
				row-key="id" @menu-handle="menuHandle" @selection-change="selectionChange">
				<el-table-column fixed type="selection" width="60" />
				<el-table-column align="center" fixed="right" label="操作" width="140">

					<template #default="scope">
						<el-button size="small" text type="primary" @click="open_dialog(scope.row)">
							编辑
						</el-button>
						<el-divider direction="vertical" />
						<el-popconfirm title="确定删除吗？" @confirm="delete_item(scope.row, scope.$index)">
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

				<template #isSystem="{ data }">
					<el-tag :type="data.isSystem ? 'success' : 'danger'">
						{{ data.isSystem ? "是" : "否" }}
					</el-tag>
				</template>
			</scTable>
		</el-main>
		<edit ref="edit" @complete="complete" />
	</el-container>
</template>

<script>
import { defineAsyncComponent } from "vue";
export default {
	name: 'scm_ur_group',
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
	},
	data() {
		return {
			apiObj: this.$API.urgroup.list,
			tableName: 'scm_ur_group',
			list: [],
			param: {
				key: '',
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true, },
				{ prop: "namec", label: "群组名称", width: 200, align: "left", },
				{ prop: "remark", label: "备注", minWidth: 200, align: "left", },
				{ prop: "row_status", label: "数据状态", width: "80", },
				{ prop: "update_names", label: "更新人员", width: "100", },
				{ prop: "update_time", label: "更新时间", width: "160", formatter: this.$TOOL.dateTimeFormat },
				{ prop: "create_names", label: "创建人员", width: "100", },
				{ prop: "create_time", label: "创建时间", width: "160", formatter: this.$TOOL.dateTimeFormat },
			],
		};
	},
	mounted() {
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search(key) {
			if (key != null) {
				this.param.key = key;
			}
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.urgroup.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.urgroup.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.urgroup.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.urgroup.delete, this.selection);
		},
		open_dialog(row) {
			this.$refs.edit.open(row);
		},
		selectionChange(selection) {
			this.selection = selection;
		},
		menuHandle(obj) {
			if (obj.command == "add") {
				this.open_dialog({});
			}
			if (obj.command == "edit") {
				this.open_dialog(obj.row);
			}
			if (obj.command == "delete") {
				this.delete_item(obj.row);
			}
		},
	},
};
</script>
