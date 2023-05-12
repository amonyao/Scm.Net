<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="700px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属角色" prop="parentId">
				<el-tree-select v-model="formData.parentId" placeholder="请选择所属角色" :data="parentIdOptions" collapse-tags
					check-strictly default-expand-all :style="{ width: '100%' }" />
			</el-form-item>
			<el-form-item label="角色名称" prop="namec">
				<el-input v-model="formData.namec" clearable :maxlength="30" placeholder="请输入角色名称" show-word-limit
					:style="{ width: '100%' }" />
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" :autosize="{ minRows: 2, maxRows: 4 }" :maxlength="500"
					placeholder="请输入备注" show-word-limit :style="{ width: '100%' }" type="textarea" />
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
			titleMap: {
				add: "新增",
				edit: "编辑",
			},
			isSaveing: false,
			visible: false,
			formData: {
				id: 0,
				parentId: undefined,
				namec: undefined,
				remark: undefined,
				od: 1,
			},
			rules: {
				parentId: [
					{
						required: true,
						message: "请选择所属角色",
						trigger: "change",
					},
				],
				namec: [
					{
						required: true,
						message: "请输入角色名称",
						trigger: "blur",
					},
				],
				remark: [],
			},
			parentIdOptions: [],
			parentIdProps: {
				multiple: false,
				checkStrictly: true,
				expandTrigger: "hover",
			},
		};
	},
	mounted() { },
	methods: {
		async initTree() {
			const t = await this.$API.urrole.list.get();
			let _tree = [
				{ id: "1", value: "0", label: "角色组", parentId: "0" },
			];
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
				var res = await this.$API.urrole.model.get(row.id);
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
						res = await this.$API.urrole.add.post(this.formData);
					} else {
						res = await this.$API.urrole.update.put(this.formData);
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
			this.formData = {
				id: 0,
				parentId: undefined,
				namec: undefined,
				remark: undefined,
				od: 1,
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
<style scoped>
.cur-tip {
	display: inline-block;
	padding-left: 10px;
	color: #666;
	font-size: 12px;
}
</style>
