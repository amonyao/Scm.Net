<template>
	<sc-search ref="search" @search="search">
		<template #search>
			<el-form ref="formRef" label-width="80px" :model="param">
				<el-form-item label="书籍类型" prop="types">
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
	</sc-search>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" @click="open_dialog()"><sc-icon name="sc-plus" /></el-button>
				<el-divider direction="vertical"></el-divider>
				<el-button type="primary" plain @click="do_upload">上传</el-button>
				<sc-file-import ref="import" :api-obj="importApi" :template-url="templateUrl" title="导入"
					@success="success"></sc-file-import>
				<el-divider direction="vertical"></el-divider>
				<el-button-group>
					<el-tooltip content="启用">
						<el-button type="primary" plain :disabled="selection.length == 0"
							@click="status_list(1)"><sc-icon name="sc-check-circle-line" /></el-button>
					</el-tooltip>
					<el-tooltip content="停用">
						<el-button type="primary" plain :disabled="selection.length == 0"
							@click="status_list(2)"><sc-icon name="sc-pause-circle-line" /></el-button>
					</el-tooltip>
					<el-tooltip content="删除">
						<el-button type="danger" plain :disabled="selection.length == 0" @click="delete_list"><sc-icon
								name="sc-close-circle-line" /></el-button>
					</el-tooltip>
				</el-button-group>
			</div>
			<div class="right-panel">
				<el-input v-model="param.key" clearable placeholder="关键字">
					<template #append>
						<el-button type="primary" @click="search()"><sc-icon name="sc-search" /></el-button>
					</template>
				</el-input>
				<el-button @click="show_search">高级</el-button>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :tableName="tableName" :api-obj="apiObj" :column="column" row-key="id"
				@menu-handle="menuHandle" @selection-change="selectionChange">
				<!-- 固定列-选择列 -->
				<el-table-column align="center" fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50" />
				<el-table-column label="操作" align="center" fixed="right" width="140">
					<template #default="scope">
						<el-button text type="primary" size="small" @click="open_dialog(scope.row)">
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
	</el-container>
	<edit ref="edit" @complete="complete" />
	<scUpload ref="upload" @complete="complete" :api-obj="this.$API.samplesbook.upload" />
</template>

<script>
import { defineAsyncComponent } from "vue";
import config from "@/config";

export default {
	name: 'samples_book',
	components: {
		edit: defineAsyncComponent(() => import("./edit")),
		scUpload: defineAsyncComponent(() => import("@/components/scUpload")),
	},
	data() {
		return {
			tableName: 'samples_book',
			apiObj: this.$API.samplesbook.page,
			uploadApi: this.$API.samplesbook.upload,
			importApi: this.$API.samplesbook.import,
			templateUrl: `${config.SERVER_URL}/upload/templates/示例模板.xlsx`,
			param: {
				types: 0,
				row_status: 1,
				create_time: '',
				key: ''
			},
			formData: {},
			selection: [],
			column: [
				{ label: "id", prop: "id", hide: true },
				{ prop: 'types', label: '书籍类型', width: 100, formatter: this.getTypesNames },
				{ prop: 'codes', label: '系统代码', width: 140 },
				{ prop: 'codec', label: '书籍编码', width: 100 },
				{ prop: 'names', label: '系统简称', width: 160 },
				{ prop: 'namec', label: '书籍全称', minWidth: 200, align: 'left' },
				{ prop: 'barcode', label: '条码', width: 100 },
				{ prop: 'remark', label: '备注', width: 200 },
				{ prop: 'wfa_status', label: '审批状态', width: 80 },
				{ prop: 'row_status', label: '数据状态', width: 80 },
				{ prop: 'update_time', label: '更新时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'update_names', label: '更新人员', width: 100 },
				{ prop: 'create_time', label: '创建时间', width: 160, formatter: this.$TOOL.dateTimeFormat },
				{ prop: 'create_names', label: '创建人员', width: 100 },
			],
			types_list: [this.$SCM.OPTION_ALL],
			row_status_list: [this.$SCM.OPTION_ALL]
		};
	},
	mounted() {
		this.$SCM.list_status(this.row_status_list);
		this.$SCM.list_dic(this.types_list, 'samples_demo_book_types', true);
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.samplesbook.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.samplesbook.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.samplesbook.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.samplesbook.delete, this.selection);
		},
		show_search() {
			this.$refs.search.open(this.param.key);
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
		success(res) {
			if (res == null) {
				return;
			}
			//this.$refs.upload.tip = res.message;
		},
		do_upload() {
			this.$refs.upload.open();
		},
		getTypesNames(types) {
			return this.$SCM.get_dic_names(this.types_list, types, '');
		}
	},
};
</script>