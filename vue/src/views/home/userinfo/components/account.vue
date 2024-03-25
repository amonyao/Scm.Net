<template>
	<el-card shadow="never" header="个人信息">
		<el-form ref="form" :model="form" label-width="120px" style="margin-top: 20px">
			<el-form-item label="账号">
				<el-input v-model="form.names" disabled></el-input>
				<div class="el-form-item-msg">
					账号信息用于登录，系统不允许修改
				</div>
			</el-form-item>
			<el-form-item label="姓名">
				<el-input v-model="form.namec"></el-input>
			</el-form-item>
			<el-form-item label="性别">
				<sc-select v-model="form.sex" placeholder="请选择" :data="sex_list"></sc-select>
			</el-form-item>
			<el-form-item label="手机号码">
				<el-input v-model="form.phone"></el-input>
			</el-form-item>
			<el-form-item label="电子邮件">
				<el-input v-model="form.email"></el-input>
			</el-form-item>
			<el-form-item label="个性签名">
				<el-input v-model="form.remark" type="textarea"></el-input>
			</el-form-item>
			<el-form-item>
				<el-button type="primary" @click="saveBasic">保存</el-button>
			</el-form-item>
		</el-form>
	</el-card>
</template>

<script>
export default {
	data() {
		return {
			sex_list: [],
			form: {
				id: '0',
				names: "admin@user",
				namec: "系统管理员",
				phone: '',
				email: '',
				avatar: '',
				sex: 1,
				remark: '',
				role: [],
				post: [],
			},
		};
	},
	mounted() {
		this.$SCM.list_dic(this.sex_list, 'sex', false);
		this.init();
	},
	methods: {
		async init() {
			const res = await this.$API.login.user.get();
			if (!res || res.code != 200) {
				return;
			}

			this.form = res.data;
		},
		async saveBasic() {
			if (!this.form.names) {
				this.$alert("姓名不能为空！", "提示", { type: "error" });
				return;
			}
			const user = this.$TOOL.data.get("USER_INFO");
			this.form.id = user.id;
			const res = await this.$API.login.userBasic.put(this.form);
			if (res.code == 200) {
				this.$message.success("保存成功");
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
	},
};
</script>