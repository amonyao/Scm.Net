<template>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" @click="open_dialog">
					<sc-icon name="sc-plus" />
				</el-button>
				<el-button plain type="danger" :disabled="selection.length == 0" @click="batch_del">
					<sc-icon name="sc-close" />
				</el-button>
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="param.key" clearable placeholder="关键字" />
					<el-button type="primary" @click="search"><sc-icon name="sc-search" /></el-button>
				</div>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" :api-obj="apiObj" :column="column" :menu-column="menuColumn" row-key="id"
				@selection-change="selectionChange" @menu-handle="menuHandle">
				<!-- 固定列-选择列 -->
				<el-table-column fixed type="selection" width="60" />
				<el-table-column label="#" type="index" width="50"></el-table-column>
				<el-table-column align="center" fixed="right" label="操作" width="140">
					<template #default="scope">
						<el-button size="small" text type="primary" @click="open_dialog(scope.row)">
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

				<template #status="{ data }">
					<el-tag disable-transitions :type="data.status ? 'success' : 'danger'">
						{{ data.status ? "正常" : "停用" }}
					</el-tag>
				</template>

				<template #type="{ data }">
					{{ data.type ? "图文" : "视频" }}
				</template>

				<template #tickRule="{ data }">
					{{ data.tickRule ? "单选" : "多选" }}
				</template>
			</scTable>
		</el-main>
		<modify ref="modify" @complete="complete" />
		<detail ref="detail" />
	</el-container>
</template>

<script>
import { defineAsyncComponent } from "vue";
export default {
	name: 'scm_vote',
	components: {
		modify: defineAsyncComponent(() => import("./modify")),
		detail: defineAsyncComponent(() => import("./detail")),
	},
	data() {
		return {
			apiObj: this.$API.sysvote.page,
			list: [],
			param: {
				key: "",
			},
			selection: [],
			column: [
				{
					label: "id",
					prop: "id",
					width: "200",
					sortable: true,
					hide: true,
				},
				{ prop: "title", label: "投票标题", minWidth: 200, align: 'left' },
				{ prop: "type", label: "投票类型", width: 100 },
				{ prop: "tickRule", label: "勾选规则", width: 100 },
				{ prop: "startTime", label: "开始时间", width: 160 },
				{ prop: "endTime", label: "结束时间", width: 160 },
				{ prop: "createTime", label: "创建时间", width: 160 },
			],
			menuColumn: [
				{
					command: "look",
					title: "查看",
					icon: "sc-eye-view",
					suffix: "Look",
				},
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
		//删除
		async table_del(row) {
			var res = await this.$API.sysvote.delete.delete([row.id]);
			if (res.code == 200) {
				this.$refs.table.refresh();
				this.$message.success("删除成功");
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
		//批量删除
		batch_del() {
			this.$confirm(
				`确定删除选中的 ${this.selection.length} 项吗？`,
				"提示",
				{
					type: "warning",
					confirmButtonText: "确定",
					cancelButtonText: "取消",
				}
			)
				.then(async () => {
					const loading = this.$loading();
					let ids = [];
					this.selection.forEach((element) => {
						ids.push(element.id);
					});
					var res = await this.$API.sysvote.delete.delete(ids);
					if (res.code == 200) {
						this.$refs.table.refresh();
						loading.close();
						this.$message.success("删除成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				})
				.catch(() => { });
		},
		open_dialog(row) {
			if (row.id) {
				this.$refs.modify.open(row);
			} else {
				this.$refs.modify.open();
			}
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
			if (obj.command == "look") {
				this.$refs.detail.open(obj.row);
			}
		},
	},
};
</script>
