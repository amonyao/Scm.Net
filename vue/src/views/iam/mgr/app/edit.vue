<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属用户" prop="user_id">
				<sc-select v-model="formData.user_id" placeholder="请输入所属用户"></sc-select>
			</el-form-item>
			<el-form-item label="应用代码" prop="app_code">
				<el-input v-model="formData.app_code" placeholder="请输入应用代码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="应用名称" prop="app_name">
				<el-input v-model="formData.app_name" placeholder="请输入应用名称" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="应用说明" prop="app_desc">
				<el-input v-model="formData.app_desc" placeholder="请输入应用说明" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="回调地址" prop="redirect_uri">
				<el-input v-model="formData.redirect_uri" placeholder="请输入回调地址" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="排序依据" prop="order_by">
				<sc-select v-model="formData.order_by" placeholder="请输入排序依据" :data="orderby_list"></sc-select>
			</el-form-item>
			<el-form-item label="显示更多" prop="show_more">
				<sc-select v-model="formData.show_more" placeholder="请输入显示更多" :data="showmore_list"></sc-select>
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
				user_id: [
					{ required: true, trigger: "blur", message: "请选择所属用户" },
				],
				app_code: [
					{ required: true, trigger: "blur", message: "应用代码不能为空" },
				],
				app_name: [
					{ required: true, trigger: "blur", message: "应用名称不能为空" },
				],
			},
			orderby_list: [],
			showmore_list: []
		};
	},
	mounted() {
		this.$SCM.list_dic(this.orderby_list, 'oidc_osp_orderby', false);
		this.$SCM.list_dic(this.showmore_list, 'oidc_osp_showmore', false);
	},
	methods: {
		def_data() {
			return {
				id: 0,
				user_id: '',
				app_code: '',
				app_name: '',
				app_desc: '',
				redirect_uri: '',
				app_key: '',
				app_secret: '',
				access_token: '',
				refresh_token: '',
				access_expires: '',
				refresh_expires: '',
				qty: '',
				order_by: '',
				show_more: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.iammgrapp.edit.get(row.id);
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
						res = await this.$API.iammgrapp.add.post(this.formData);
					} else {
						res = await this.$API.iammgrapp.update.put(this.formData);
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