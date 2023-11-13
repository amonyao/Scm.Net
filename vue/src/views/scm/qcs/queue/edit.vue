<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="" prop="header_id"> 
	<el-input 
		v-model="formData.header_id" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="detail_id"> 
	<el-input 
		v-model="formData.detail_id" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="排队序号" prop="idx"> 
	<el-input 
		v-model="formData.idx" 
		placeholder="请输入排队序号" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="排队号码" prop="codec"> 
	<el-input 
		v-model="formData.codec" 
		placeholder="请输入排队号码" 
		:maxlength="8" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="联系人" prop="contact"> 
	<el-input 
		v-model="formData.contact" 
		placeholder="请输入联系人" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="证件号码" prop="label"> 
	<el-input 
		v-model="formData.label" 
		placeholder="请输入证件号码" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="联系电话" prop="phone"> 
	<el-input 
		v-model="formData.phone" 
		placeholder="请输入联系电话" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="优先级" prop="level"> 
	<el-input 
		v-model="formData.level" 
		placeholder="请输入优先级" 
		:maxlength="4" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="呼叫次数" prop="calling"> 
	<el-input 
		v-model="formData.calling" 
		placeholder="请输入呼叫次数" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="处理状态" prop="handle"> 
	<el-input 
		v-model="formData.handle" 
		placeholder="请输入处理状态" 
		:maxlength="4" 
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
				id: '0',
				header_id:'', 
detail_id:'', 
idx:'', 
codec:'', 
contact:'', 
label:'', 
phone:'', 
level:'', 
calling:'', 
handle:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmqcsqueue.edit.get(row.id);
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
						res = await this.$API.scmqcsqueue.add.post(this.formData);
					} else {
						res = await this.$API.scmqcsqueue.update.put(this.formData);
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