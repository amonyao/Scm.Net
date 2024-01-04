<template>
	<el-container class="is-vertical">
		<sc-search>
			<template #filter>
				<el-button type="primary" @click="open_dialog">
					<sc-icon icon="sc-plus" />
				</el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" :disabled="selection.length == 0" @click="status_list(1)">
							<sc-icon icon="sc-check-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" :disabled="selection.length == 0" @click="status_list(2)">
							<sc-icon icon="sc-pause-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0" @click="delete_list">
							<sc-icon icon="sc-close-circle-line" />
						</el-button>
					</el-tooltip>
				</el-button-group>
			</template>
		</sc-search>
		<el-main class="nopadding">
			<scTable ref="table" :table-name="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<!-- 固定列-选择列 -->
				<el-table-column fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
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
			apiObj: this.$API.ur_position.page,
			tableName: 'scm_ur_position',
			list: [],
			param: {
				key: ""
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ label: "岗位编号", prop: "codec", width: 120 },
				{ label: "岗位名称", prop: "namec", minWidth: 200, align: "left" },
				{ label: "排序", prop: "od", width: "80" },
				{ label: "数据状态", prop: "row_status", width: "80" },
				{ label: "更新时间", prop: "update_time", width: "160", sortable: true, formatter: this.$TOOL.dateTimeFormat },
				{ label: "更新人员", prop: "update_names", width: "100" },
				{ label: "创建时间", prop: "create_time", width: "160", sortable: true, formatter: this.$TOOL.dateTimeFormat },
				{ label: "创建人员", prop: "create_names", width: "100" },
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
			this.$SCM.status_item(this, this.$API.ur_position.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.ur_position.status, this.selection, status);
		},
		//删除
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.ur_position.delete, row);
		},
		//批量删除
		delete_list() {
			this.$SCM.delete_list(this, this.$API.ur_position.delete, this.selection);
		},
		open_dialog(row) {
			if (row.id) {
				this.$refs.edit.open(row);
			} else {
				this.$refs.edit.open();
			}
		},
		selectionChange(selection) {
			this.selection = selection;
		},
		menuHandle(obj) {
			if (obj.command == 'add') {
				this.open_dialog({})
			}
			if (obj.command == 'edit') {
				this.open_dialog(obj.row)
			}
			if (obj.command == 'delete') {
				this.delete_item(obj.row)
			}
		},
	},
};
</script>
