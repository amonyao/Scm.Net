<template>
	<el-drawer title="用户反馈" v-model="visible" :size="400" append-to-body destroy-on-close>
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="登录标识" prop="key">
				<el-input v-model="formData.key" placeholder="请输入登录标识" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="联合用户" prop="oidc_id">
				<el-input v-model="formData.oidc_id" placeholder="请输入联合用户" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="应用名称" prop="app_id">
				<el-input v-model="formData.app_id" placeholder="请输入应用名称" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="服务名称" prop="osp_id">
				<el-input v-model="formData.osp_id" placeholder="请输入服务名称" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="OAuthID" prop="oauth_id">
				<el-input v-model="formData.oauth_id" placeholder="请输入OAuthID" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="用户" prop="user">
				<el-input v-model="formData.user" placeholder="请输入用户" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="昵称" prop="name">
				<el-input v-model="formData.name" placeholder="请输入昵称" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="性别" prop="sex">
				<el-input v-model="formData.sex" placeholder="请输入性别" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="头像" prop="avatar">
				<el-input v-model="formData.avatar" placeholder="请输入头像" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="错误代码" prop="err_code">
				<el-input v-model="formData.err_code" placeholder="请输入错误代码" :maxlength="8" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="错误描述" prop="err_msg">
				<el-input v-model="formData.err_msg" placeholder="请输入错误描述" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="登录次数" prop="qty">
				<el-input v-model="formData.qty" placeholder="请输入登录次数" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="电话" prop="phone">
				<el-input v-model="formData.phone" placeholder="请输入电话" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="邮件" prop="email">
				<el-input v-model="formData.email" placeholder="请输入邮件" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</el-drawer>
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
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: 0,
				key: '',
				oidc_id: '',
				app_id: '',
				osp_id: '',
				oauth_id: '',
				user: '',
				name: '',
				sex: '',
				avatar: '',
				access_token: '',
				refresh_token: '',
				expires_in: '',
				err_code: '',
				err_msg: '',
				qty: '',
				phone: '',
				email: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.iamlogoauth.edit.get(row.id);
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
						res = await this.$API.iamlogoauth.add.post(this.formData);
					} else {
						res = await this.$API.iamlogoauth.update.put(this.formData);
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