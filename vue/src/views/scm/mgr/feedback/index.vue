<template>
	<sc-search ref="search" @search="search">
		<template #search>
			<el-form ref="formRef" label-width="80px" :model="param">
				<el-form-item label="所属机构" prop="unit_id">
					<sc-select v-model="param.unit_id" placeholder="请选择" :data="unit_list" />
				</el-form-item>
				<el-form-item label="反馈类型" prop="types">
					<sc-select v-model="param.types" placeholder="请选择" :data="types_list" />
				</el-form-item>
				<el-form-item label="数据状态" prop="row_status">
					<sc-select v-model="param.row_status" placeholder="请选择" :data="row_status_list" />
				</el-form-item>
				<el-form-item label="创建时间" prop="create_time">
					<el-date-picker v-model="param.create_time" type="datetimerange" range-separator="至"
						start-placeholder="开始日期" end-placeholder="结束日期" />
				</el-form-item>
				<el-form-item label="搜索内容">
					<el-input v-model="param.key" clearable placeholder="关键字" />
				</el-form-item>
			</el-form>
		</template>
	</sc-search>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button icon="el-icon-plus" type="primary" @click="open_dialog()" />
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :tableName="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<el-table-column align="center" fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
				<el-table-column label="操作" align="center" fixed="right" width="70">

					<template #default="scope">
						<el-button text type="primary" size="small" @click="open_newtab(scope.row)">
							详情
						</el-button>
					</template>
				</el-table-column>
			</scTable>
		</el-main>
	</el-container>
</template>

<script>
export default {
	name: 'scm_mgr_feedback',
	data() {
		return {
			tableName: 'mgr_feedback',
			apiObj: this.$API.mgrfeedback.page,
			list: [],
			param: {
				unit_id: '0',
				types: 0,
				row_status: 1,
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'unit_id', label: '机构', width: 100, formatter: this.getUnitNames },
				{ prop: 'types', label: '反馈类型', width: 100, formatter: this.getTypesNames },
				{ prop: 'title', label: '标题', width: 100 },
				{ prop: 'remark', label: '内容', width: 100 },
				{ prop: 'handle', label: '处理状态', width: 100 },
				{ prop: 'resolve', label: '解决状态', width: 100 },
				{ prop: 'row_status', label: '数据状态', width: 100 },
				{ prop: 'update_time', label: '更新时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'update_names', label: '更新人员', width: 100 },
				{ prop: 'create_time', label: '创建时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'create_names', label: '创建人员', width: 100 },
			],
			row_status_list: [],
			types_list: [],
			unit_list: [],
		};
	},
	mounted() {
		this.$SCM.list_status(this.row_status_list);
		this.$SCM.list_dic(this.types_list, 'feedback_type', true);
		this.$SCM.list_option(this.unit_list, this.$API.mgrunit.option, 0, true);
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		open_newtab(row) {
			if (!row.id) {
				return;
			}
			this.$router.push({ path: '/scm/mgr/feedback/detail', query: { 'id': row.id } });
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
		getUnitNames(unitId) {
			return this.$SCM.get_option_names(this.unit_list, unitId, '');
		},
		getTypesNames(types) {
			return this.$SCM.get_dic_names(this.types_list, types, '');
		},
		show_search() {
			this.$refs.search.open(this.param.key);
		}
	},
};
</script>
