<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="类别" prop="pid"> 
	<el-input 
		v-model="formData.pid" 
		placeholder="请输入类别" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="types"> 
	<el-input 
		v-model="formData.types" 
		placeholder="请输入" 
		:maxlength="4" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="modes"> 
	<el-input 
		v-model="formData.modes" 
		placeholder="请输入" 
		:maxlength="4" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="" prop="codes"> 
	<el-input 
		v-model="formData.codes" 
		placeholder="请输入" 
		:maxlength="256" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="短链编码" prop="codec"> 
	<el-input 
		v-model="formData.codec" 
		placeholder="请输入短链编码" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="系统名称" prop="names"> 
	<el-input 
		v-model="formData.names" 
		placeholder="请输入系统名称" 
		:maxlength="256" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="展示名称" prop="namec"> 
	<el-input 
		v-model="formData.namec" 
		placeholder="请输入展示名称" 
		:maxlength="256" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="文件路径" prop="path"> 
	<el-input 
		v-model="formData.path" 
		placeholder="请输入文件路径" 
		:maxlength="16" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="文件摘要" prop="hash"> 
	<el-input 
		v-model="formData.hash" 
		placeholder="请输入文件摘要" 
		:maxlength="256" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="扩展名" prop="exts"> 
	<el-input 
		v-model="formData.exts" 
		placeholder="请输入扩展名" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="文件大小" prop="size"> 
	<el-input 
		v-model="formData.size" 
		placeholder="请输入文件大小" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="引用数量" prop="refs"> 
	<el-input 
		v-model="formData.refs" 
		placeholder="请输入引用数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="标签数量" prop="tags"> 
	<el-input 
		v-model="formData.tags" 
		placeholder="请输入标签数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="收藏数量" prop="favs"> 
	<el-input 
		v-model="formData.favs" 
		placeholder="请输入收藏数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="备注" prop="remark"> 
	<el-input 
		v-model="formData.remark" 
		placeholder="请输入备注" 
		:maxlength="256" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="文件创建时间" prop="file_create_time"> 
	<el-input 
		v-model="formData.file_create_time" 
		placeholder="请输入文件创建时间" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="文件修改时间" prop="file_modify_time"> 
	<el-input 
		v-model="formData.file_modify_time" 
		placeholder="请输入文件修改时间" 
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
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
	    def_data(){
		    return {
				id: '0',
				pid:'', 
types:'', 
modes:'', 
codes:'', 
codec:'', 
names:'', 
namec:'', 
path:'', 
hash:'', 
exts:'', 
size:'', 
refs:'', 
tags:'', 
favs:'', 
remark:'', 
file_create_time:'', 
file_modify_time:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.fesdocfile.edit.get(row.id);
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
						res = await this.$API.fesdocfile.add.post(this.formData);
					} else {
						res = await this.$API.fesdocfile.update.put(this.formData);
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