<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="机构全称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="机构简称" prop="names">
				<el-input v-model="formData.names" placeholder="请输入" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="固话" prop="telephone">
				<el-input v-model="formData.telephone" placeholder="请输入" :maxlength="24" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="联系人" prop="contact">
				<el-input v-model="formData.contact" placeholder="请输入联系人" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="手机" prop="cellphone">
				<el-input v-model="formData.cellphone" placeholder="请输入手机号码" :maxlength="16" show-word-limit
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
			titleMap: {
				add: "新增",
				edit: "编辑",
			},
			visible: false,
			isSaveing: false,
			formData: {
				id: '0',
				names: '',
				namec: '',
				contact: '',
				cellphone: '',
				telephone: ''
			},
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.urunit.edit.get(row.id);
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
						res = await this.$API.urunit.add.post(this.formData);
					} else {
						res = await this.$API.urunit.update.put(this.formData);
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
			this.formData = {
				id: '0',
				names: '',
				namec: '',
				contact: '',
				cellphone: '',
				telephone: ''
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
