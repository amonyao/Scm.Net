<template>
	<el-container>
		<el-header style="height: auto;display: block;">
			<div>
				<el-row>
					<el-col :span="8">
						123
					</el-col>
					<el-col :span="8">
						123
					</el-col>
					<el-col :span="8">
						123
					</el-col>
					<el-col :span="8">
						123
					</el-col>
					<el-col :span="8">
						123
					</el-col>
					<el-col :span="8">
						123
					</el-col>
				</el-row>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :table-name="tableName" :api-obj="apiObj" :column="column" row-key="id"
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
	</el-container>
	<bookSelector ref="bookSelector" @complete="complete" />
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
	name: 'samplespodetail',
	components: {
		bookSelector: defineAsyncComponent(() => import("../book/selector")),
	},
	data() {
		return {
			tableName: 'samplespodetail',
			apiObj: this.$API.samplespodetail.page,
			list: [],
			param: {
				option_id: '0',
				row_status: 1,
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'book_id', label: '书籍编码', width: 100 },
				{ prop: 'book_id', label: '书籍名称', width: 100, align: 'left' },
				{ prop: 'need_qty', label: '需求数量', width: 100, align: 'right' },
				{ prop: 'real_qty', label: '实际数量', width: 100, align: 'right' },
				{ prop: 'remark', label: '备注', minWidth: 140, align: 'left' },
				{ prop: "row_status", label: "数据状态", width: 80, },
				{ prop: "update_names", label: "更新人员", width: 100, },
				{ prop: "update_time", label: "更新时间", width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: "create_names", label: "创建人员", width: 100, },
				{ prop: "create_time", label: "创建时间", width: 160, formatter: this.$TOOL.dateTimeFormat },
			],
			row_status_list: [this.$SCM.OPTION_ALL],
			option_list: [this.$SCM.OPTION_ALL],
		};
	},
	mounted() {
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
			this.$SCM.status_item(this, this.$API.samplespodetail.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.samplespodetail.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.samplespodetail.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.samplespodetail.delete, this.selection);
		},
		show_search() {
			this.$refs.search.open(this.param.key);
		},
		open_dialog(row) {
			this.$refs.bookSelector.open(row);
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
