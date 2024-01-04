<template>
	<el-container>
		<scSearch>
			<template #search>
				<el-form ref="formRef" label-width="100px" :model="param" :inline="true">
					<el-form-item label="查询选项" prop="types">
						<sc-select v-model="param.types" placeholder="请选择" :data="types_list" />
					</el-form-item>
					<el-form-item label="数据状态" prop="row_status">
						<sc-select v-model="param.row_status" placeholder="请选择" :data="row_status_list" />
					</el-form-item>
					<el-form-item label="创建时间" prop="create_time">
						<el-date-picker v-model="param.create_time" type="datetimerange" range-separator="至"
							start-placeholder="开始日期" end-placeholder="结束日期" />
					</el-form-item>
				</el-form>
			</template>
		</scSearch>
		<el-header>
			<div class="left-panel">
				<el-button icon="el-icon-plus" type="primary" @click="open_dialog()" />
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="param.key" clearable placeholder="关键字" />
					<el-button icon="el-icon-search" type="primary" @click="search" />
				</div>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :api-obj="apiObj" :column="column" row-key="id" @menu-handle="menuHandle"
				@selection-change="selectionChange">
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
	data() {
		return {
			apiObj: this.$API.msgfeedback.page,
			list: [],
			param: {
				types: '',
				row_status: 1,
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
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
		};
	},
	mounted() {
		this.$SCM.list_status(this.row_status_list);
		this.$SCM.list_dic(this.types_list, 'feedback_type', true);
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
			this.$router.push({ path: '/scm/msg/feedback/detail', query: { 'id': row.id } });
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
		getTypesNames(types) {
			return this.$SCM.get_dic_names(this.types_list, types, '');
		}
	},
};
</script>
