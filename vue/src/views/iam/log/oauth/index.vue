<template>
	<el-container class="is-vertical">
		<sc-search @search="search">
			<template #filter>
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
			</template>
		</sc-search>
		<el-main class="nopadding">
			<scTable ref="table" :api-obj="apiObj" :column="column" row-key="id" @menu-handle="menuHandle"
				@selection-change="selectionChange">
				<el-table-column align="center" fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
				<el-table-column label="操作" align="center" fixed="right" width="80">
					<template #default="scope">
						<el-button text type="primary" size="small" @click="open_dialog(scope.row)">
							详情
						</el-button>
					</template>
				</el-table-column>
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
			apiObj: this.$API.iamlogoauth.page,
			list: [],
			param: {
				option_id: '0',
				row_status: '1',
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'oidc_id', label: '联合代码', width: 100 },
				{ prop: 'app_id', label: '应用名称', width: 100 },
				{ prop: 'osp_id', label: '登录网站', width: 100 },
				{ prop: 'oauth_id', label: 'UnionID', width: 100 },
				{ prop: 'user', label: '用户', width: 100 },
				{ prop: 'name', label: '昵称', width: 100 },
				{ prop: 'sex', label: '性别', width: 100 },
				{ prop: 'avatar', label: '头像', width: 100 },
				{ prop: 'err_code', label: '错误代码', width: 100 },
				{ prop: 'err_msg', label: '错误描述', width: 100 },
				{ prop: 'qty', label: '登录次数', width: 100 },
				{ prop: "update_names", label: "更新人员", width: "100", },
				{ prop: "update_time", label: "更新时间", width: "160", formatter: this.$TOOL.dateTimeFormat },
				{ prop: "create_names", label: "创建人员", width: "100", },
				{ prop: "create_time", label: "创建时间", width: "160", formatter: this.$TOOL.dateTimeFormat },
			],
			row_status_list: [this.$SCM.OPTION_ALL],
			option_list: [this.$SCM.OPTION_ALL],
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
			this.$SCM.status_item(this, this.$API.iamlogoauth.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.iamlogoauth.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.iamlogoauth.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.iamlogoauth.delete, this.selection);
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
	},
};
</script>
