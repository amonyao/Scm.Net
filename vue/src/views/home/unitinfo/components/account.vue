<template>
	<el-card shadow="never" header="机构信息">
		<el-form ref="form" :model="form" label-width="120px" style="margin-top: 20px">
			<el-form-item label="账号">
				<el-input v-model="form.names" disabled></el-input>
				<div class="el-form-item-msg">
					账号信息用于标识登录用户的人性机构，系统不允许修改
				</div>
			</el-form-item>
			<el-form-item label="机构名称">
				<el-input v-model="form.namec"></el-input>
			</el-form-item>
			<el-form-item label="固话">
				<el-input v-model="form.telephone"></el-input>
			</el-form-item>
			<el-form-item label="联系人">
				<el-input v-model="form.contact"></el-input>
			</el-form-item>
			<el-form-item label="联系手机">
				<el-input v-model="form.cellphone"></el-input>
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
				names: "user",
				namec: "系统管理员",
				telephone: "",
				contact: "",
				cellphone: "",
			},
		};
	},
	mounted() {
		this.init();
	},
	methods: {
		async init() {
			const res = await this.$API.login.unitWord.get();
			if (!res || res.code != 200) {
				return;
			}

			this.form = res.data;
		},
		async saveBasic() {
			if (!this.form.namec) {
				this.$alert("机构名称不能为空！", "提示", { type: "error" });
				return;
			}
			//const user = this.$TOOL.data.get("USER_INFO");
			//this.form.id = user.id;
			const res = await this.$API.login.unitBasic.put(this.form);
			if (res.code == 200) {
				this.$message.success("保存成功");
			} else {
				this.$alert(res.message, "提示", { type: "error" });
			}
		},
	},
};
</script>