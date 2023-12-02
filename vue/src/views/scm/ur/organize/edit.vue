<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" :destroy-on-close="true" width="800px"
		@close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属组织" prop="parentId">
				<el-tree-select v-model="formData.parentId" :data="parentIdOptions" :default-expand-all="true"
					:check-strictly="true" :style="{ width: '100%' }" />
			</el-form-item>
			<el-row>
				<el-col :span="12">
					<el-form-item label="组织名称" prop="namec">
						<el-input v-model="formData.namec" placeholder="请输入组织名称" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="组织编号">
						<el-input v-model="formData.codec" placeholder="请输入组织编号" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="组织负责人" prop="leaderUser">
						<el-input v-model="formData.leaderUser" placeholder="请输入组织负责人" clearable></el-input></el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="联系电话" prop="leaderMobile">
						<el-input v-model="formData.leaderMobile" placeholder="请输入联系电话" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="联系邮箱" prop="leaderEmail">
						<el-input v-model="formData.leaderEmail" placeholder="请输入联系邮箱" clearable></el-input>
					</el-form-item>
				</el-col>
			</el-row>

			<el-form-item label="排序" prop="od" required>
				<el-slider v-model="formData.od" :max="100" show-input :step="1" />
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
	emits: ['complete'],
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑", },
			isSaveing: false,
			visible: false,
			formData: this.def_data(),
			rules: {
				parentId: [
					{ required: true, trigger: "change", pattern: this.$SCM.REGEX_ID, message: "请选择所属组织", },
				],
				parentIdList: [
					{ required: true, type: "array", trigger: "change", message: "请至少选择一个所属组织", },
				],
				namec: [
					{ required: true, trigger: "blur", message: "请输入组织名称", },
				],
				leaderUser: [
					{ required: true, trigger: "blur", message: "请输入组织负责人", },
				],
				leaderMobile: [
					{ required: true, trigger: "blur", message: "请输入联系电话", },
				],
				leaderEmail: [],
			},
			parentIdOptions: [],
			parentIdProps: {
				expandTrigger: "hover",
				//checkStrictly: true,
			},
		};
	},
	mounted() { },
	methods: {
		def_data() {
			return {
				id: '0',
				parentId: undefined,
				namec: "",
				leaderUser: "",
				leaderMobile: "",
				leaderEmail: "",
				od: 1,
			}
		},
		async initTree() {
			const t = await this.$API.urorganize.list.get();
			let _tree = [{ id: "1", value: "0", label: "请选择", parentId: "0" }];
			t.data.some((m) => {
				_tree.push({
					id: m.id,
					value: m.id,
					label: m.namec,
					parentId: m.parentId,
				});
			});
			this.parentIdOptions = this.$TOOL.changeTree(_tree);
		},
		async open(row) {
			this.initTree();
			if (!row) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.urorganize.model.get(row.id);
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
						res = await this.$API.urorganize.add.post(
							this.formData
						);
					} else {
						res = await this.$API.urorganize.update.put(
							this.formData
						);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("操作成功");
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
