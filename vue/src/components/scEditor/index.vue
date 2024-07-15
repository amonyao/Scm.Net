<template>
	<el-container class="notepad">
		<el-header class="notepad-toolbar">
			<Toolbar :editor="editorRef" :defaultConfig="toolbarConfig" :mode="mode" />
		</el-header>
		<el-main class="nopadding notepad-container">
			<div class="notepad-content">
				<div class="notepad-title" v-if="titleVisible">
					<input v-model="titleRef" :placeholder="titlePlaceholder" @keydown.enter="handleEnter" />
				</div>
				<Editor style="height: 900px;overflow-y: hidden;" v-model="contentRef" :defaultConfig="editorConfig"
					:mode="mode" :placeholder="contentPlaceholder" @onCreated="handleCreated" />
			</div>
		</el-main>
	</el-container>
</template>

<script>
import '@wangeditor/editor/dist/css/style.css';

import { onBeforeUnmount, ref, shallowRef } from 'vue';
import { Editor, Toolbar } from '@wangeditor/editor-for-vue';

export default {
	props: {
		mode: { type: String, default: 'simple' },
		titlePlaceholder: { type: String, default: '请输入标题...' },
		contentPlaceholder: { type: String, default: '请输入内容...' },
		title: { type: String, default: '' },
		titleVisible: { type: Boolean, default: true },
		content: { type: String, default: '' },
	},
	components: { Editor, Toolbar },
	setup(props) {
		// 编辑器实例，必须用 shallowRef
		const editorRef = shallowRef()

		// 内容 HTML
		const titleRef = ref(props.title);
		const setTitle = (title) => {
			titleRef.value = title;
		}
		const getTitle = () => {
			return titleRef.value;
		}

		const contentRef = ref(props.content);
		const setContent = (content) => {
			contentRef.value = content;
		}
		const getContent = () => {
			return contentRef.value;
		}

		const toolbarConfig = {}
		const editorConfig = { placeholder: '请输入内容...' }

		// 组件销毁时，也及时销毁编辑器
		onBeforeUnmount(() => {
			const editor = editorRef.value
			if (editor == null) return
			editor.destroy()
		})

		const handleCreated = (editor) => {
			editorRef.value = editor;
		}

		const handleEnter = () => {
			const editor = editorRef.value
			if (editor == null) return
			editor.focus();
		}

		return {
			editorRef,
			titleRef,
			contentRef,
			toolbarConfig,
			editorConfig,
			handleCreated,
			handleEnter,
			setTitle,
			getTitle,
			setContent,
			getContent
		};
	}
}
</script>
<style type="scss" scoped>
.notepad {
	overflow-y: hidden;

	.notepad-toolbar {
		/* margin: 0 auto; */
		overflow: hidden;
		background-color: #fff;
	}

	.notepad-container {
		overflow-y: auto;
		position: relative;

		.notepad-content {
			width: 860px;
			margin: 0 auto;
			background-color: var(--el-bg-color);
			padding: 20px 50px 50px 50px;
			border: 1px solid var(--el-border-color-light);
			box-shadow: var(--el-box-shadow-light);

			.notepad-title {
				padding: 20px 0;
				border-bottom: 1px solid var(--el-border-color-light);

				input {
					font-size: 30px;
					border: 0;
					outline: none;
					width: 100%;
					line-height: 1;
				}
			}

			.notepad-body {
				height: 900px;
				overflow-y: hidden;
			}
		}
	}
}
</style>
<style>
.w-e-toolbar {
	flex-wrap: nowrap;
}
</style>