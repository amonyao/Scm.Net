<template>
	<el-container>
		<scSearch>
			<template #search>
				<el-form ref="formRef" label-width="100px" :model="param" :inline="true">
					<el-form-item label="国别" prop="option_id">
						<sc-select v-model="param.nation_id" placeholder="请选择国别" :data="nation_list"
							@change="getDynasty()"></sc-select>
					</el-form-item>
					<el-form-item label="朝代" prop="dynasty_id">
						<sc-select v-model="param.dynasty_id" placeholder="请选择朝代" :data="dynasty_list"></sc-select>
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
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" icon="el-icon-circle-check" plain :disabled="selection.length == 0"
							@click="status_list(1)"></el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" icon="el-icon-circle-close" plain :disabled="selection.length == 0"
							@click="status_list(2)"></el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" icon="el-icon-delete" plain :disabled="selection.length == 0"
							@click="delete_list"></el-button>
					</el-tooltip>
				</el-button-group>
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
			apiObj: this.$API.cmsresauthor.page,
			list: [],
			param: {
				nation_id: '0',
				dynasty_id: '0',
				row_status: '1',
				create_time: '',
				key: ''
			},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'nation_names', label: '国别', width: 100 },
				{ prop: 'dynasty_names', label: '朝代', width: 100 },
				{ prop: 'codec', label: '编码', width: 100 },
				{ prop: 'names', label: '简称', width: 100 },
				{ prop: 'namec', label: '全称', width: 100 },
				{ prop: 'namee', label: 'Name', width: 100 },
				{ prop: 'od', label: '显示排序', width: 80 },
				{ prop: 'row_status', label: '数据状态', width: 80 },
				{ prop: 'update_names', label: '更新人员', width: 100 },
				{ prop: 'update_time', label: '更新时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'create_names', label: '创建人员', width: 100 },
				{ prop: 'create_time', label: '创建时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },

			],
			row_status_list: [this.$SCM.OPTION_ALL],
			nation_list: [this.$SCM.OPTION_ALL],
			dynasty_list: [this.$SCM.OPTION_ALL],
		};
	},
	mounted() {
		this.getNation();
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.cmsresauthor.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.cmsresauthor.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.cmsresauthor.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.cmsresauthor.delete, this.selection);
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
		async getNation() {
			var res = await this.$API.cmsresnation.option.get();
			this.$SCM.prepare(this.nation_list, res, true);
		},
		async getDynasty() {
			if (!this.param.nation_id) {
				return;
			}

			var res = await this.$API.cmsresdynasty.option.get({ 'nation_id': this.param.nation_id });
			this.$SCM.prepare(this.dynasty_list, res, true);
		}
	},
};
</script>
