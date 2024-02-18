<template>
    <div style="background-color: aliceblue;">
        <div id="editor"></div>
    </div>
</template>
<script>
import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header';
import List from '@editorjs/list';

export default {
    data() {
        return {
            content: '',
            wangValue: '',
            wangDisabled: false,
            editor: ''
        };
    },
    mounted() {
        this.init();
    },
    methods: {
        init() {
            this.editor = new EditorJS({
                holder: 'editor',
                autofocus: true,
                placeholder: 'Press Tab',
                tools: {
                    header: Header,
                    list: List
                },
                i18n: {
                    messages: {
                        ui: {
                            blockTunes: {
                                toggler: {
                                    'Click to tune': '点击调整'
                                }
                            },
                            inlineToolbar: {
                                converter: {
                                    'Convert to': '转换为'
                                }
                            },
                            toolbar: {
                                toolbox: {
                                    'Add': '点击添加'
                                }
                            },
                            popover: {
                                'Filter': '过滤',
                                'Nothing found': '未找到'
                            }
                        },
                        toolNames: {
                            'Text': '文本',
                            'Bold': '加粗',
                            'Italic': '斜体',
                            'Heading': '标题',
                            'List': '列表'
                        },
                        tools: {
                            paragraph: {
                                'Press Tab': '输入内容'
                            },
                        },
                        blockTunes: {
                            delete: {
                                'Delete': '删除'
                            },
                            moveUp: {
                                'Move up': '上移'
                            },
                            moveDown: {
                                'Move down': '下移'
                            },
                        }
                    }
                }
            });
        },
        save() {
            this.editor.save().then((outputData) => {
                console.log('Article data: ', outputData)
            }).catch((error) => {
                console.log('Saving failed: ', error)
            });
        }
    },
};
</script>