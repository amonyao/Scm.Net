<template>
	<el-container>
		<el-aside width="260px" v-loading="showGrouploading">
			<el-container>
				<el-header>
					<el-input placeholder="输入关键字进行过滤" v-model="groupFilterText" clearable></el-input>
					<el-button type="primary" round icon="el-icon-search" class="add-column"></el-button>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="group" class="menu" node-key="id" default-expand-all :data="group"
						:filter-node-method="groupFilterNode" @node-click="groupClick">
						<template #default="{ node, data }">
							<span class="custom-tree-node">
								<span class="label">{{ node.label }}</span>
								<span class="code">{{ data.code }}</span>
								<span class="do">
									<el-icon><el-icon-edit /></el-icon>
									<el-icon><el-icon-delete /></el-icon>
								</span>
							</span>
						</template>
					</el-tree>
				</el-main>
			</el-container>
		</el-aside>

		<el-main>
			<el-container>
				<div class="list-body">
					<sc-list :header="header" :data="list" :hide-do="true" class="qcs-list" :can-selected="false">
						<template #item="{ item }">
							<el-button type="primary" @click="queuing(item)" style="width: 100%; height: 50px;">
								{{ item.namec }}
							</el-button>
						</template>
					</sc-list>
				</div>
			</el-container>
		</el-main>
	</el-container>
</template>
<script>
const signalR = require("@microsoft/signalr");
export default {
	data() {
		return {
			showGrouploading: false,
			groupFilterText: "",
			group: [],
			defaultParam: { type: 1 },
			list: [],
			header: '',
		};
	},
	mounted() {
		this.getGroup({ type: this.defaultParam.type });
		this.init();
	},
	methods: {
		complete() {
			this.$refs.table.refresh();
		},
		search() {
			this.$refs.table.upData(this.param);
		},
		async status_item(e, row) {
			this.$SCM.status_item(this, this.$API.scmqcsqueue.status, row, row.row_status);
		},
		status_list(status) {
			this.$SCM.status_list(this, this.$API.scmqcsqueue.status, this.selection, status);
		},
		async delete_item(row) {
			this.$SCM.delete_item(this, this.$API.scmqcsqueue.delete, row);
		},
		delete_list() {
			this.$SCM.delete_list(this, this.$API.scmqcsqueue.delete, this.selection);
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
		//加载树数据
		async getGroup(param) {
			this.showGrouploading = true;
			const res = await this.$API.scmqcsheader.list.get(param);
			this.showGrouploading = false;
			let _tree = [];//{ id: "1", value: "0", label: "所有", parentId: "0" }];
			res.data.some((m) => {
				_tree.push({
					id: m.id,
					value: m.id,
					label: m.namec,
					code: m.codec,
					type: m.types,
					parentId: m.pid,
				});
			});
			this.group = this.$TOOL.changeTree(_tree);
		},
		//树过滤
		groupFilterNode(value, data) {
			if (!value) return true;
			return data.label.indexOf(value) !== -1;
		},
		//树点击事件
		groupClick(data) {
			var params = {
				id: data.id,
			};
			this.header = data.label;
			this.listDetail(params);
		},
		// 获取队列
		async listDetail(param) {
			var res = await this.$API.scmqcsdetail.list.get(param);
			if (!res || res.code != 200) {
				return;
			}
			this.list = res.data;
		},
		// 取号
		async queuing(row) {
			if (!row || !row.id) {
				return;
			}

			var res = await this.$API.scmqcsqueue.queuing.get({ id: row.id });
			if (!res || res.code != 200) {
				return;
			}
			this.sendMessage(res.data);
		},
		init() {
			var url = this.$CONFIG.SERVER_URL + '/QcsHub';
			this.connection = new signalR.HubConnectionBuilder()
				.withUrl(url)
				.build();

			this.connection.start().then(() => {
				this.connection.on("ReceiveMessage", (user, message) => {
					this.messages.push(`${user}: ${message}`);
				});
			});
		},
		sendMessage(dvo) {
			this.$message.success("取号成功：" + dvo.codec);
			if (dvo) {
				this.connection.invoke("SendMessage", dvo.detail_id);
			}
		},
	},
};
</script>
<style scoped>
.list-body {
	width: 100%;
	display: flex;
	justify-content: center;
	align-items: center;
}

.qcs-list {
	width: 600px;
}
</style>
