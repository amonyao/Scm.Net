<template>
	<sc-dialog v-model="visible" show-fullscreen title="用户授权" width="500px" @close="close">
		<el-scrollbar max-height="400px">
			<el-tree ref="tree" node-key="id" :data="menuList" default-expand-all show-checkbox
				:default-checked-keys="checkedList" />
		</el-scrollbar>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</sc-dialog>
</template>
<script>
export default {
	data() {
		return {
			isSaveing: false,
			visible: false,
			menuloading: false,
			menuList: [],
			checkedList: [],
			formData: {}
		};
	},
	mounted() { },
	methods: {
		async getMenu() {
			this.menuloading = true;
			var res = await this.$API.devmenu.list.get();
			this.menuloading = false;
			let _tree = [];
			res.data.some((m) => {
				_tree.push({
					id: m.id,
					value: m.id,
					label: m.namec,
					parentId: m.parentId,
				});
			});
			this.menuList = this.$TOOL.changeTree(_tree);
		},
		async open(row) {
			if (!row || !row.id) {
				return;
			}

			this.formData.unit_id = row.unit_id;
			this.formData.role_id = row.id;

			this.getMenu();

			var res = await this.$API.mgrroleauth.byrole.get(row.id);
			let checkedList = [];
			res.data.some((m) => {
				checkedList.push(m.auth_id);
			});
			this.checkedList = checkedList;

			this.visible = true;
		},
		async save() {
			let nodes = this.$refs.tree.getCheckedNodes();
			if (nodes.length < 1) {
				this.$message.warning("请选择要授权的资源");
				return;
			}

			let auths = [];
			nodes.some((m) => {
				auths.push(m.id);
			});

			this.formData.auth_list = auths;
			this.isSaveing = true;
			let res = await this.$API.mgrroleauth.save.post(this.formData);
			this.isSaveing = false;
			if (res.code == 200) {
				this.$emit("complete");
				this.visible = false;
				this.$message.success("操作成功");
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
		close() {
			this.formData = {
				id: '0',
				parentId: 0,
				namec: undefined,
				remark: undefined,
				od: 1,
			};
			this.visible = false;
		},
	},
};
</script>
