<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="服务编码" prop="code">
				<el-input v-model="formData.code" placeholder="请输入服务编码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="服务名称" prop="name">
				<el-input v-model="formData.name" placeholder="请输入服务名称" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="服务图标" prop="icon">
				<el-input v-model="formData.icon" placeholder="请输入服务图标" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="协议版本" prop="ver">
				<sc-select v-model="formData.ver" placeholder="请输入协议版本" :data="ver_list"></sc-select>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input-number v-model="formData.od" placeholder="请输入显示排序" :min="0"></el-input-number>
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
				code: [
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
				name: [
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
			},
			ver_list: []
		};
	},
	mounted() {
		this.$SCM.list_dic(this.ver_list, 'oidc_oauth_version', false);
	},
	methods: {
		def_data() {
			return {
				id: 0,
				code: '',
				name: '',
				ver: '',
				icon: '',
				od: '',
				qty: '',
				row_system: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.iammgrosp.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === 0) {
						res = await this.$API.iammgrosp.add.post(this.formData);
					} else {
						res = await this.$API.iammgrosp.update.put(this.formData);
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

<style>
.el-select {
	width: 100%;
}
</style>