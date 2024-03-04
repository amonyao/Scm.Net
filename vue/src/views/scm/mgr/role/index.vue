<template>
	<el-container class="is-vertical">
		<sc-search @search="search">
			<template #search>
				<el-form ref="formRef" label-width="100px" :model="param" :rules="rules" :inline="true">
					<el-form-item label="机构" prop="unit_id">
						<sc-select v-model="param.unit_id" placeholder="请选择" :data="unit_list" />
					</el-form-item>
					<el-form-item label="数据状态" prop="row_status">
						<sc-select v-model="param.row_status" clearable placeholder="请选择" :data="row_status_list" />
					</el-form-item>
					<el-form-item label="创建时间" prop="create_time">
						<el-date-picker v-model="param.create_time" type="datetimerange" range-separator="至"
							start-placeholder="开始日期" end-placeholder="结束日期">
						</el-date-picker>
					</el-form-item>
					<el-form-item label="搜索内容">
						<el-input v-model="param.key" clearable placeholder="关键字" />
					</el-form-item>
					<el-form-item>
						<el-button type="primary" @click="search">
							<sc-icon name="sc-search" />查询
						</el-button>
					</el-form-item>
				</el-form>
			</template>

			<template #filter>
				<el-button type="primary" @click="open_dialog">
					<sc-icon name="sc-plus" />
				</el-button>
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
			<scTable ref="table" :tableName="tableName" :api-obj="apiObj" :column="column" hide-pagination is-tree
				row-key="id" @menu-handle="menuHandle" @selection-change="selectionChange">
				<!-- 固定列-选择列 -->
				<el-table-column fixed type="selection" width="60" />
				<el-table-column align="center" fixed="right" label="操作" width="210">

					<template #default="scope">
						<el-button size="small" text type="primary" @click="open_dialog(scope.row)">
							编辑
						</el-button>
						<el-divider direction="vertical" />
						<el-button size="small" text type="primary" @click="open_auth(scope.row)">
							权限
						</el-button>
						<el-divider direction="vertical" />
						<el-popconfirm title="确定删除吗？" @confirm="delete_item(scope.row, scope.$index)">
							<template #reference>
								<el-button text :disabled="scope.row.isSystem" type="primary"
									size="small">删除</el-button>
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

				<template #maxLength="{ data }">
					<el-tag type="info">
						{{ data.maxLength == 0 ? "不限制" : data.maxLength }}
					</el-tag>
				</template>
			</scTable>
		</el-main>
		<edit ref="edit" @complete="complete" />
		<auth ref="auth" />
	</el-container>
</template>

<script>
import { defineAsyncComponent } from "vue";
export default {
	name: 'scm_mgr_role',
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
		auth: defineAsyncComponent(() => import("./components/roleAuth")),
	},
	data() {
		return {
			tableName: 'scm_mgr_role',
			apiObj: this.$API.mgrrole.list,
			list: [],
			param: {
				unit_id: '0',
				row_status: 0,
				key: '',
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true, },
				{ label: "角色名称", prop: "namec", width: 200, align: "left", },
				{ label: "所属机构", prop: "unit_id", width: 200, align: "left", formatter: this.getUnitNames },
				{ label: "备注", prop: "remark", minWidth: 200, align: "left", },
				{ label: "数据状态", prop: "row_status", width: "80", },
				{ label: "更新时间", prop: "update_time", width: "160", sortable: true, formatter: this.$TOOL.dateTimeFormat },
				{ label: "创建时间", prop: "create_time", width: "160", sortable: true, formatter: this.$TOOL.dateTimeFormat },
			],
			rules: [],
			unit_list: [],
			row_status_list: [],
		};
	},
	mounted() {
		this.getUnit();
		this.$SCM.list_status(this.row_status_list, true);
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.mgrrole.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.mgrrole.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.mgrrole.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.mgrrole.delete, this.selection);
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
		async getUnit() {
			let res = await this.$API.mgrunit.option.get(0);
			this.$SCM.prepare(this.unit_list, res, true);
		},
		open_auth(row) {
			this.$refs.auth.open(row);
		},
		getUnitNames(key) {
			return this.$SCM.get_option_names(this.unit_list, key, '');
		}
	},
};
</script>
