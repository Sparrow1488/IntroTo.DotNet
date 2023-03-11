# Git

1. Одна задача - одна ветка

```textline
develop -> develop/task
```

2. Если для решения задачи нужно разбить ее на подзадачи, под них так же создаются отдельные ветки производные от основной

```textile
develop ->
    develop/task ->
        develop/task/sub-1
        develop/task/sub-2
```

3. После решения задачи отправляется PR (*pull request*) на ветку `develop`

# Branches

```textile
{stage}/{service}/{task}
```

**Stages**: main, hotfix, release(?), develop, feature

**Service**: api, identity, storage etc. Использовать, если проект построен на микросервисах

**Task**: краткое название задачи или тег
