<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="模板类型" prop="types">
				<sc-select v-model="formData.types" placeholder="请选择模板类型" :data="types_list"></sc-select>
			</el-form-item>
			<el-form-item label="模板代码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入模板代码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="模板名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入模板名称" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="标题模板" prop="head">
				<el-input v-model="formData.head" placeholder="请输入标题模板" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="内容模板" prop="body">
				<el-input v-model="formData.body" placeholder="请输入内容模板" :maxlength="512" show-word-limit clearable
					type="textarea"></el-input>
			</el-form-item>
			<el-form-item label="声明模板" prop="foot">
				<el-input v-model="formData.foot" placeholder="请输入声明模板" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="文件模板" prop="file">
				<el-input v-model="formData.file" placeholder="请输入文件模板" :maxlength="64" show-word-limit
					clearable></el-input>
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
			types_list: [],
			rules: {
				types: [
					{ required: true, trigger: "change", pattern: this.$SCM.REGEX_INT, message: "请选择应用", },
				],
				codec: [
					{ required: true, trigger: "blur", message: "请输入模板代码！", },
				],
				namec: [
					{ required: true, trigger: "blur", message: "请输入模板名称！", },
				],
				head: [
					{ required: true, trigger: "blur", message: "请输入标题模板！", },
				],
				body: [
					{ required: true, trigger: "blur", message: "请输入内容模板！", },
				],
			},
		};
	},
	mounted() {
		this.$SCM.list_dic(this.types_list, 'sms_types', false);
	},
	methods: {
		def_data() {
			return {
				id: '0',
				types: '',
				codec: '',
				namec: '',
				head: '',
				body: '',
				foot: '',
				file: '',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmressms.edit.get(row.id);
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
						res = await this.$API.scmressms.add.post(this.formData);
					} else {
						res = await this.$API.scmressms.update.put(this.formData);
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