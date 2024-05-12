<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="系统代码" prop="codes"> 
	<el-input 
		v-model="formData.codes" 
		placeholder="请输入系统代码" 
		:maxlength="16" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="活动编码" prop="codec"> 
	<el-input 
		v-model="formData.codec" 
		placeholder="请输入活动编码" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="系统名称" prop="names"> 
	<el-input 
		v-model="formData.names" 
		placeholder="请输入系统名称" 
		:maxlength="64" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="活动名称" prop="namec"> 
	<el-input 
		v-model="formData.namec" 
		placeholder="请输入活动名称" 
		:maxlength="128" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="活动类型" prop="types"> 
	<el-input 
		v-model="formData.types" 
		placeholder="请输入活动类型" 
		:maxlength="4" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="起始时间" prop="f_time"> 
	<el-input 
		v-model="formData.f_time" 
		placeholder="请输入起始时间" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="结束时间" prop="t_time"> 
	<el-input 
		v-model="formData.t_time" 
		placeholder="请输入结束时间" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="商品ID" prop="spu_id"> 
	<el-input 
		v-model="formData.spu_id" 
		placeholder="请输入商品ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="规格ID" prop="spec_id"> 
	<el-input 
		v-model="formData.spec_id" 
		placeholder="请输入规格ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="数量" prop="qty"> 
	<el-input 
		v-model="formData.qty" 
		placeholder="请输入数量" 
		:maxlength="11" 
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
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
	    def_data(){
		    return {
				id: '0',
				codes:'', 
codec:'', 
names:'', 
namec:'', 
types:'', 
f_time:'', 
t_time:'', 
spu_id:'', 
spec_id:'', 
qty:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.posseheader.edit.get(row.id);
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
						res = await this.$API.posseheader.add.post(this.formData);
					} else {
						res = await this.$API.posseheader.update.put(this.formData);
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