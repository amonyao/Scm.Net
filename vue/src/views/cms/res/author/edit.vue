<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="国别" prop="nation_id">
				<sc-select v-model="formData.nation_id" placeholder="请选择国别" :data="nation_list"
					@change="getDynasty()"></sc-select>
			</el-form-item>
			<el-form-item label="朝代" prop="dynasty_id">
				<sc-select v-model="formData.dynasty_id" placeholder="请选择朝代" :data="dynasty_list"></sc-select>
			</el-form-item>
			<el-form-item label="编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入编码" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="全称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入全称" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="简称" prop="names">
				<el-input v-model="formData.names" placeholder="请输入简称" :maxlength="16" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="Name" prop="namee">
				<el-input v-model="formData.namee" placeholder="请输入Name" :maxlength="256" show-word-limit
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
				nation_id: [
					{ required: true, trigger: "blur", pattern: this.$SCM.REGEX_ID, message: "请选择国别" },
				],
				dynasty_id: [
					{ required: true, trigger: "blur", pattern: this.$SCM.REGEX_ID, message: "请选择朝代" },
				],
				codec: [
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
				namec: [
					{ required: true, trigger: "blur", message: "名称不能为空" },
				],
			},
			nation_list: [this.$SCM.OPTION_ONE],
			dynasty_list: [this.$SCM.OPTION_ONE],
		};
	},
	mounted() {
		this.getNation();
	},
	methods: {
		def_data() {
			return {
				id: '0',
				nation_id: '0',
				dynasty_id: '0',
				od: 0,
				codec: '',
				names: '',
				namec: '',
				namee: '',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.cmsresauthor.edit.get(row.id);
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
						res = await this.$API.cmsresauthor.add.post(this.formData);
					} else {
						res = await this.$API.cmsresauthor.update.put(this.formData);
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
		async getNation() {
			this.$SCM.list_option(this.nation_list, this.$API.cmsresnation.option, {}, false);
		},
		async getDynasty() {
			if (!this.formData.nation_id) {
				return;
			}

			this.formData.dynasty_id = '0';
			this.$SCM.list_option(this.dynasty_list, this.$API.cmsresdynasty.option, { 'nation_id': this.formData.nation_id }, false);
		}
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>