<template>
	<el-container>
		<scSearch>
			<template #search>
				<el-form ref="formRef" label-width="100px" :model="param" :inline="true">
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
				<el-button type="primary" @click="open_dialog()"><sc-icon icon="sc-plus"/></el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" plain :disabled="selection.length == 0"
							@click="status_list(1)"><sc-icon icon="sc-check-circle-line"/></el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" plain :disabled="selection.length == 0"
							@click="status_list(2)"><sc-icon icon="sc-pause-circle-line"/></el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0"
							@click="delete_list"><sc-icon icon="sc-close-circle-line"/></el-button>
					</el-tooltip>
				</el-button-group>
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="param.key" clearable placeholder="关键字" />
					<el-button type="primary" @click="search"><sc-icon icon="sc-search"/></el-button>
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
						<el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
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
			tableName: 'dev_uid',
			apiObj: this.$API.devuid.page,
			list: [],
			param: {
				option_id: '',
				row_status: 1,
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'k', label: '键', minWidth: 140, align: 'left' },
				{ prop: 'v', label: '当前值', width: 80, align: 'right' },
				{ prop: 'c', label: '缓存大小', width: 80, align: 'right' },
				{ prop: 'b', label: '缓冲大小', width: 80, align: 'right' },
				{ prop: 'l', label: '数值长度', width: 80, align: 'right' },
				{ prop: 'm', label: '前置掩码', width: 80, align: 'left' },
				{ prop: 'p', label: '后置掩码', width: 80, align: 'left' },
				{ prop: 't', label: '访问时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
			],
			row_status_list: [],
			option_list: [],
		};
	},
	mounted() {
		this.$SCM.list_status(this.row_status_list);
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.devuid.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.devuid.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.devuid.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.devuid.delete, this.selection);
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
