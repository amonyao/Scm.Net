<template>
	<el-container>
		<scSearch>
			<template #search>
				<el-form ref="formRef" label-width="100px" :model="param" :inline="true">
					<el-form-item label="机构" prop="unit_id">
						<sc-select v-model="param.unit_id" placeholder="请选择" :change="changeUnit()" :data="unit_list" />
					</el-form-item>
					<el-form-item label="所属角色" prop="role_id">
						<el-tree-select v-model="param.role_id" placeholder="请选择所属角色" :data="role_list" collapse-tags
							check-strictly default-expand-all :style="{ width: '100%' }" />
					</el-form-item>
					<el-form-item label="数据状态" prop="row_status">
						<sc-select v-model="param.row_status" clearable placeholder="请选择" :data="row_status_list" />
					</el-form-item>
					<el-form-item label="创建时间" prop="create_time">
						<el-date-picker v-model="param.create_time" type="datetimerange" range-separator="至"
							start-placeholder="开始日期" end-placeholder="结束日期">
						</el-date-picker>
					</el-form-item>
				</el-form>
			</template>
		</scSearch>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" @click="open_dialog">
					<sc-icon icon="sc-plus" />
				</el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(1)">
							<sc-icon icon="sc-check-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" plain :disabled="selection.length == 0" @click="status_list(2)">
							<sc-icon icon="sc-pause-circle-line" />
						</el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0" @click="delete_list">
							<sc-icon icon="sc-close-circle-line" />
						</el-button>
					</el-tooltip>
				</el-button-group>
				<el-divider direction="vertical"></el-divider>
				<el-button type="danger" plain :disabled="selection.length != 1" @click="pwdreset">密码重置</el-button>
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="param.key" clearable placeholder="登录账号" />
					<el-button type="primary" @click="search"><sc-icon icon="sc-search" /></el-button>
				</div>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :tableName="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<!-- 固定列-选择列 -->
				<el-table-column fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
				<el-table-column align="center" fixed="right" label="操作" width="210">
					<template #default="scope">
						<el-button size="small" text type="primary" @click="open_dialog(scope.row)">
							编辑
						</el-button>
						<el-divider direction="vertical" />
						<el-button size="small" text type="primary" @click="open_role(scope.row)">
							授权
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
				<template #avatar="{ data }">
					<el-avatar :src="$CONFIG.SERVER_URL + data.avatar" size="small"></el-avatar>
				</template>
				<template #OrganizeObj="{ data }">
					{{ data.organizeObj?.name }}
				</template>
			</scTable>
		</el-main>
	</el-container>
	<edit ref="edit" @complete="complete" />
	<role ref="role" />
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
		role: defineAsyncComponent(() => import("./components/userRole")),
	},
	data() {
		return {
			tableName: 'scm_mgr_user',
			apiObj: this.$API.mgruser.page,
			list: [],
			param: {
				unit_id: '0',
				role_id: '0',
				row_status: 0,
				key: '',
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true, },
				{ label: "头像", prop: "avatar", width: "60", },
				{ label: "系统编码", prop: "codes", width: "120", align: "left", },
				{ label: "员工工号", prop: "codec", width: "120", align: "left", },
				{ label: "登录账户", prop: "user", width: "120", align: "right", },
				{ label: "系统名称", prop: "names", width: "120", align: "left", },
				{ label: "展示姓名", prop: "namec", width: "120", align: "left", },
				{ label: "所属机构", prop: "unit_names", align: "left", width: "200", },
				{ label: "性别", prop: "sex", width: "80", formatter: this.getSexNames },
				{ label: "手机号码", prop: "cellphone", width: "120", align: "right" },
				{ label: "固话", prop: "telephone", width: "120", align: "right" },
				{ label: "数据状态", prop: "row_status", width: "80" },
				{ label: "创建时间", prop: "create_time", width: "160", sortable: true, formatter: this.$TOOL.dateTimeFormat },
			],
			unit_list: [],
			role_list: [this.$SCM.OPTION_ALL],
			row_status_list: [],
			sex_list: [],
		};
	},
	mounted() {
		this.getUnit();
		this.$SCM.list_status(this.row_status_list, true);
		this.$SCM.list_sex(this.sex_list, true);
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.mgruser.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.mgruser.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.mgruser.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.mgruser.delete, this.selection);
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
		pwdreset() {
			this.$confirm(
				`确定要重置 ${this.selection.length} 项的密码吗？`,
				"提示",
				{
					type: "warning",
					confirmButtonText: "确定",
					cancelButtonText: "取消",
				}
			).then(async () => {
				const loading = this.$loading();
				let ids = [];
				this.selection.forEach((element) => {
					ids.push(element.id);
				});
				var res = await this.$API.mgruser.passreset.put(ids);
				if (res.code == 200) {
					this.$refs.table.refresh();
					loading.close();
					this.$message.success("重置成功");
				} else {
					this.$alert(res.message, "提示", { type: "error" });
				}
			}).catch(() => { });
		},
		getUnit() {
			this.$SCM.list_option(this.unit_list, this.$API.mgrunit.option, {}, true);
		},
		changeUnit() {
			if (this.param.unit_id == '0') {
				this.role_list.length = 0;
				this.role_list.push(this.$SCM.OPTION_ALL);
				return;
			}
			this.getRole();
		},
		//加载树数据
		async getRole() {
			this.showGrouploading = true;
			const res = await this.$API.mgrrole.option.get(this.param.unit_id);
			this.showGrouploading = false;
			let _tree = [{ id: "1", value: "0", label: "所有", parentId: "0" }];
			res.data.some((m) => {
				_tree.push({
					id: m.id,
					value: m.value,
					label: m.label,
					parentId: m.parentId,
				});
			});
			this.role_list = this.$TOOL.changeTree(_tree);
		},
		open_role(row) {
			this.$refs.role.open(row);
		},
		getSexNames(key) {
			return this.$SCM.get_dic_names(this.sex_list, key, '');
		}
	},
};
</script>
