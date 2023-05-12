<template>
	<sc-dialog v-model="visible" show-fullscreen title="授权角色" width="500px" @close="close">
		<el-scrollbar max-height="400px">
			<el-tree ref="tree" node-key="id" :data="data" default-expand-all show-checkbox
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
			data: [],
			checkedList: [],
			formData: {}
		};
	},
	mounted() {
	},
	methods: {
		async getRole() {
			const role = await this.$API.mgrrole.option.get(this.formData.unit_id);
			let roleArr = [];
			role.data.forEach(function (m) {
				roleArr.push({
					id: m.id,
					value: m.value,
					label: m.label,
					parentId: m.parentId,
				});
			});
			this.data = this.$TOOL.changeTree(roleArr);
		},
		async open(row) {
			if (!row || !row.id) {
				return;
			}

			this.formData.unit_id = row.unit_id;
			this.formData.user_id = row.id;

			this.getRole();

			var res = await this.$API.mgrUserRole.byuser.get(row.id);
			let checkedList = [];
			res.data.some((m) => {
				checkedList.push(m.role_id);
			});
			this.checkedList = checkedList;

			this.visible = true;
		},
		async save() {
			let nodes = this.$refs.tree.getCheckedNodes();
			if (nodes.length < 1) {
				this.$message.warning("请选择要授权的角色");
				return;
			}

			let roles = [];
			nodes.forEach((item) => {
				roles.push(item.id);
			});

			this.formData.role_list = roles;
			this.isSaveing = true;
			let res = await this.$API.mgrUserRole.save.post(this.formData);
			this.isSaveing = false;
			if (res.code == 200) {
				this.$message.success("授权成功");
				this.visible = false;
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
		close() {
			this.$refs.tree.setCheckedNodes([]);
			this.visible = false;
		},
	},
};
</script>
