<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属园区" prop="area_id"> 
	<el-input 
		v-model="formData.area_id" 
		placeholder="请输入所属园区" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="所属楼宇" prop="build_id"> 
	<el-input 
		v-model="formData.build_id" 
		placeholder="请输入所属楼宇" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="所属楼层" prop="floor_id"> 
	<el-input 
		v-model="formData.floor_id" 
		placeholder="请输入所属楼层" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="所属房间" prop="room_id"> 
	<el-input 
		v-model="formData.room_id" 
		placeholder="请输入所属房间" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="系统代码" prop="codes"> 
	<el-input 
		v-model="formData.codes" 
		placeholder="请输入系统代码" 
		:maxlength="16" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="水利编码" prop="codec"> 
	<el-input 
		v-model="formData.codec" 
		placeholder="请输入水利编码" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="系统名称" prop="names"> 
	<el-input 
		v-model="formData.names" 
		placeholder="请输入系统名称" 
		:maxlength="16" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="水利名称" prop="namec"> 
	<el-input 
		v-model="formData.namec" 
		placeholder="请输入水利名称" 
		:maxlength="32" 
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
				area_id:'', 
build_id:'', 
floor_id:'', 
room_id:'', 
codes:'', 
codec:'', 
names:'', 
namec:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.ymsdevhydraulic.edit.get(row.id);
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
						res = await this.$API.ymsdevhydraulic.add.post(this.formData);
					} else {
						res = await this.$API.ymsdevhydraulic.update.put(this.formData);
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