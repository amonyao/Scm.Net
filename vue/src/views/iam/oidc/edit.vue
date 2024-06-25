<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="" prop="code"> 
	<el-input 
		v-model="formData.code" 
		placeholder="请输入" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="osp_id"> 
	<el-input 
		v-model="formData.osp_id" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="oauth_id"> 
	<el-input 
		v-model="formData.oauth_id" 
		placeholder="请输入" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="user"> 
	<el-input 
		v-model="formData.user" 
		placeholder="请输入" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="name"> 
	<el-input 
		v-model="formData.name" 
		placeholder="请输入" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="avatar"> 
	<el-input 
		v-model="formData.avatar" 
		placeholder="请输入" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="phone"> 
	<el-input 
		v-model="formData.phone" 
		placeholder="请输入" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="email"> 
	<el-input 
		v-model="formData.email" 
		placeholder="请输入" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="qty"> 
	<el-input 
		v-model="formData.qty" 
		placeholder="请输入" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="access_token"> 
	<el-input 
		v-model="formData.access_token" 
		placeholder="请输入" 
		:maxlength="128" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="access_expires"> 
	<el-input 
		v-model="formData.access_expires" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="refresh_token"> 
	<el-input 
		v-model="formData.refresh_token" 
		placeholder="请输入" 
		:maxlength="128" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="refresh_expires"> 
	<el-input 
		v-model="formData.refresh_expires" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
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
			titleMap: {add: "新增",edit: "编辑"},
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
		def_data(){
			return {
				id: 0,
				code:'', 
osp_id:'', 
oauth_id:'', 
user:'', 
name:'', 
avatar:'', 
phone:'', 
email:'', 
qty:'', 
access_token:'', 
access_expires:'', 
refresh_token:'', 
refresh_expires:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.iamoidc.edit.get(row.id);
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
						res = await this.$API.iamoidc.add.post(this.formData);
					} else {
						res = await this.$API.iamoidc.update.put(this.formData);
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