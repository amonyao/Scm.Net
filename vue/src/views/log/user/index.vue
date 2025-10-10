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
				<el-divider direction="vertical"></el-divider>
				<el-button type="primary" @click="open_owner()" :disabled="selection.length != 1">修改负责人</el-button>
			</div>
			<div class="right-panel">
				<el-input v-model="param.key" clearable placeholder="关键字">
					<template #append>
						<el-button type="primary" @click="search"><sc-icon name="sc-search" /></el-button>
					</template>
				</el-input>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :table-name="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<el-table-column fixed type="selection" width="60" />
			</scTable>
		</el-main>
	</el-container>
</template>

<script>
export default {
	name: 'scm_log_user',
	data() {
		return {
			tableName: 'scm_log_user',
			apiObj: this.$API.scmloguser.page,
			param: {
				key: "",
			},
			list: [],
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: "client_names", label: "终端类型", width: 80 },
				{ prop: "type_names", label: "登录类型", width: 80 },
				{ prop: "mode_names", label: "登录方式", width: 80 },
				{ prop: "time", label: "操作时间", width: 160, align: 'left', formatter: this.$TOOL.dateTimeFormat },
				{ prop: "ip", label: "IP", width: 140, align: 'right' },
				{ prop: "result", label: "执行结果", width: 80 },
				{ prop: "remark", label: "说明", minWidth: 160, align: 'left' },
			],
		};
	},
	mounted() { },
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.urorganize.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.urorganize.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.urorganize.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.urorganize.delete, this.selection);
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
				this.table_del(obj.row);
			}
		},
		open_owner() {
			if (this.selection.length != 1) {
				return;
			}

			this.$refs.editOwner.open(this.selection[0]);
		},
	},
};
</script>