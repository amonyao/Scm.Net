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
<el-form-item label="书籍ID" prop="book_id"> 
	<el-input 
		v-model="formData.book_id" 
		placeholder="请输入书籍ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="需求数量" prop="need_qty"> 
	<el-input 
		v-model="formData.need_qty" 
		placeholder="请输入需求数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="实际数量" prop="real_qty"> 
	<el-input 
		v-model="formData.real_qty" 
		placeholder="请输入实际数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="备注" prop="remark"> 
	<el-input 
		v-model="formData.remark" 
		placeholder="请输入备注" 
		:maxlength="128" 
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
					{ required: true, trigger: "blur", message: "编码应4至32个字符", pattern: this.$SCM.REGEX_CODEC },
				],
				namec: [
					{ required: true, trigger: "blur", message: "名称不能为空" },
					{ required: true, trigger: "blur", message: "名称应4至64个字符", pattern: this.$SCM.REGEX_NAMEC },
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
book_id:'', 
need_qty:'', 
real_qty:'', 
remark:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.samplespodetail.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (!valid) {
					return;
				}

				this.isSaveing = true;
				let res = null;
				if (this.formData.id === '0') {
					res = await this.$API.samplespodetail.add.post(this.formData);
				} else {
					res = await this.$API.samplespodetail.update.put(this.formData);
				}
				this.isSaveing = false;

				if (res.code == 200) {
					this.$emit("complete");
					this.visible = false;
					this.$message.success("保存成功");
				} else {
					this.$alert(res.message, "提示", { type: "error" });
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

<style scoped>
.el-select {
	width: 100%;
}
</style>