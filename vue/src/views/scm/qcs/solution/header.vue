<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="上级方案" prop="pid">
				<el-tree-select placeholder="请选择上级方案" clearable filterable default-expand-all :check-strictly="true"
					highlight-current :indent="24" v-model="formData.pid" :data="parentIdOptions"
					:style="{ width: '100%' }" />
			</el-form-item>
			<el-form-item label="方案编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入方案编码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="方案名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入方案名称" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
		</el-form>

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
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "方案编码不能为空" },
				],
				namec: [
					{ required: true, trigger: "blur", message: "方案名称不能为空" },
				],
			},
			parentIdOptions: [],
			parentIdProps: {
				multiple: false,
				checkStrictly: true,
				expandTrigger: "hover",
			},
			defaultParam: { type: 1 },
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: '0',
				pid: '0',
				codec: '',
				namec: '',
			}
		},
		async initTree(param) {
			const t = await this.$API.scmqcsheader.list.get(param);
			let _tree = [
				{ id: "1", value: "0", label: "一级栏目", parentId: "0" },
			];
			t.data.some((m) => {
				_tree.push({
					id: m.id,
					value: m.id,
					label: m.namec,
					parentId: m.pid,
				});
			});
			this.parentIdOptions = this.$TOOL.changeTree(_tree);
		},
		async open(row) {
			this.defaultParam.type = row.type;
			this.initTree({ type: this.defaultParam.type });

			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmqcsheader.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.scmqcsheader.add.post(this.formData);
					} else {
						res = await this.$API.scmqcsheader.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("保存成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>

<style scoped>
.el-select {
	width: 100%;
}
</style>