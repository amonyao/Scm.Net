<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="类型" prop="types">
				<el-input v-model="formData.types" placeholder="请输入类型" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入名称" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="上级ID" prop="pid">
				<el-input v-model="formData.pid" placeholder="请输入上级ID" :maxlength="20" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="顶级ID" prop="tid">
				<el-input v-model="formData.tid" placeholder="请输入顶级ID" :maxlength="20" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="样式" prop="style_id">
				<el-input v-model="formData.style_id" placeholder="请输入样式" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input-number v-model="formData.od" placeholder="请输入显示排序"></el-input-number>
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
				namec: [
					{ required: true, trigger: "blur", message: "名称不能为空" },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: 0,
				types: '',
				od: '',
				namec: '',
				pid: '',
				tid: '',
				qty: '',
				style_id: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.cmsrescat.edit.get(row.id);
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
						res = await this.$API.cmsrescat.add.post(this.formData);
					} else {
						res = await this.$API.cmsrescat.update.put(this.formData);
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