<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="" prop="codes"> 
	<el-input 
		v-model="formData.codes" 
		placeholder="请输入" 
		:maxlength="16" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="codec"> 
	<el-input 
		v-model="formData.codec" 
		placeholder="请输入" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="item_need_qty"> 
	<el-input 
		v-model="formData.item_need_qty" 
		placeholder="请输入" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="item_real_qty"> 
	<el-input 
		v-model="formData.item_real_qty" 
		placeholder="请输入" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="unit_need_qty"> 
	<el-input 
		v-model="formData.unit_need_qty" 
		placeholder="请输入" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="unit_real_qty"> 
	<el-input 
		v-model="formData.unit_real_qty" 
		placeholder="请输入" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="handle"> 
	<el-input 
		v-model="formData.handle" 
		placeholder="请输入" 
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
item_need_qty:'', 
item_real_qty:'', 
unit_need_qty:'', 
unit_real_qty:'', 
handle:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.possoorderheader.edit.get(row.id);
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
						res = await this.$API.possoorderheader.add.post(this.formData);
					} else {
						res = await this.$API.possoorderheader.update.put(this.formData);
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