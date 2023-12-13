<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="机构编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入机构编码" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="机构全称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入机构全称" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="机构简称" prop="names">
				<el-input v-model="formData.names" placeholder="请输入机构简称" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="固话" prop="telephone">
				<el-input v-model="formData.telephone" placeholder="请输入固话" :maxlength="24" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="联系人" prop="contact">
				<el-input v-model="formData.contact" placeholder="请输入联系人" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="联系手机" prop="cellphone">
				<el-input v-model="formData.cellphone" placeholder="请输入联系手机" :maxlength="16" show-word-limit
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
					{ required: true, trigger: "blur", message: "请输入机构编码", },
				],
				namec: [
					{ required: true, trigger: "blur", message: "请输入机构全称", },
				],
				contact: [
					{ required: true, trigger: "blur", message: "请输入联系人", },
				],
				cellphone: [
					{ required: true, trigger: "blur", message: "请输入联系手机", },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: '0',
				codec: '',
				names: '',
				namec: '',
				contact: '',
				cellphone: '',
				telephone: ''
			};
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.mgrunit.edit.get(row.id);
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
						res = await this.$API.mgrunit.add.post(this.formData);
					} else {
						res = await this.$API.mgrunit.update.put(this.formData);
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
